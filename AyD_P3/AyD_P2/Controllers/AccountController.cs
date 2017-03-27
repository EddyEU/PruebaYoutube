using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using AyD_P2.Models;

namespace AyD_P2.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        ModeloDBEntities _db = new ModeloDBEntities();

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager )
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set 
            { 
                _signInManager = value; 
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            if (Session["codigo_usuario"] != null)
            {
                using (ModeloDBEntities db = new ModeloDBEntities())
                {
                    var codigoUsuario = (int)Session["codigo_usuario"];
                    var usuario = db.USUARIO.Where(x => x.cod_usuario == codigoUsuario).FirstOrDefault();

                    if (usuario != null)
                    {
                        usuario.estado = "0";
                        db.SaveChanges();

                        Session.Abandon();
                    }
                }
            }
            
            //return RedirectToAction("Login", "Account");
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel modelo)
        {
                var usuarioprueba = Int32.Parse(modelo.CodigoUsuario);
                var usuario = _db.USUARIO.Where(x => x.cod_cliente == usuarioprueba && x.usuario1 == modelo.Usuario && x.contrasenia == modelo.Password).FirstOrDefault();

                if (usuario == null )
                {
                    ModelState.AddModelError("", "Intento de inicio de sesión no válido.");
                    return View();
                }else
                {
                    var cuenta = _db.CUENTA.Where(x => x.cod_cliente == usuario.cod_cliente).FirstOrDefault();

                    if (cuenta == null)
                    {
                        ModelState.AddModelError("", "Usuario sin cuenta existente.");
                        return View();
                    }
                    else
                    {
                        Session["codigo_usuario"] = usuario.cod_usuario;
                        Session["usuario"] = usuario.usuario1;
                        Session["no_cuenta"] = cuenta.no_cuenta;
                        Session["codigo_cliente"] = usuario.cod_cliente;

                        usuario.estado = "1";
                        _db.SaveChanges();
                        return RedirectToAction("Index", "Home");
                    }
                }
            
        }

        //------------------------------------------------------------------------------
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            if (Session["codigo_usuario"] != null)
            {
               /* using (ModeloDBEntities db = new ModeloDBEntities())
                {*/
                    var codigoUsuario = (int)Session["codigo_usuario"];
                    var usuario = _db.USUARIO.Where(x => x.cod_usuario == codigoUsuario).FirstOrDefault();

                    if (usuario != null)
                    {
                        usuario.estado = "0";
                        _db.SaveChanges();

                        Session.Abandon();
                    }
               // }
            }
            return View();
        }


        
        //------------------------------------------------------------------------------
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel modelo)
        {
            /*using (ModeloDBEntities db = new ModeloDBEntities())
            {*/

                var cliente = _db.CLIENTE.Where(x => x.nombre_cliente == modelo.Nombre && x.correo == modelo.Email).FirstOrDefault();

                if (cliente == null)  // cliente existe
                {
                    ModelState.AddModelError("", "El cliente no existe");
                }
                else
                {
                    var usuario = _db.USUARIO.Where(x => x.usuario1 == modelo.Usuario).FirstOrDefault();

                    if (usuario == null) //usuario no duplicado
                    {
                        var codigoCliente = _db.USUARIO.Where(x => x.cod_cliente == cliente.cod_cliente).FirstOrDefault();

                        if (codigoCliente == null) //cliente ya tiene un usuario.
                        {

                            if (verificarInserciónRegistro(modelo.Usuario, modelo.Password, cliente.cod_cliente))
                            {
                                ModelState.AddModelError("", "Registro realizado");
                            }
                            else
                            {
                                ModelState.AddModelError("", "Registro no realizado");
                            }

                            var usuario2 = _db.USUARIO.Where(x => x.usuario1 == modelo.Usuario && x.contrasenia == modelo.Password).FirstOrDefault();
                            //ModelState.AddModelError("", "Tu código de usuario es " + usuario2.cod_usuario);
                            ModelState.AddModelError("", "Tu código de usuario es " + cliente.cod_cliente);
                        }
                        else
                        {
                            ModelState.AddModelError("", "El cliente ya tiene asociado un usuario");
                        }

                    }
                    else
                    {
                        ModelState.AddModelError("", "Usuario ya existe");
                    }
                        
                //}
                    
            }
            return View();
        }

        public bool verificarInserciónRegistro(string usuario, string password, int cod_cliente)
        {

            _db.USUARIO.Add(new USUARIO
            {
                usuario1 = usuario,
                contrasenia = password,
                estado = "0",
                cod_cliente = cod_cliente
            });

            var status = _db.SaveChanges();

            if (status != 0)
            {
                return true;
            }
            return false;
        }

        #region Aplicaciones auxiliares
        // Se usa para la protección XSRF al agregar inicios de sesión externos
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}