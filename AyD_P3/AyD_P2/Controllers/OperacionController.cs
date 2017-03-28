using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AyD_P2.Models;
using System.Threading.Tasks;
using System.Diagnostics;

namespace AyD_P2.Controllers
{
    public class OperacionController : Controller
    {

        ModeloDBEntities _db = new ModeloDBEntities();

        public ActionResult Index()
        {
            if (Session["codigo_usuario"] == null)
            {
                return RedirectToAction("Login", "Account");//:*
            }
            return View();
        }

        // GET: Servicio
        public ActionResult Servicio()
        {
            if (Session["codigo_usuario"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Servicio(Operacion modelo)
        {
            var cliente = Int32.Parse(Session["codigo_cliente"].ToString());

            if (verificarSaldoCuentaCredito(cliente, modelo.Monto))
            {

                var cuenta = _db.CUENTA.Where(x => x.cod_cliente == cliente).FirstOrDefault();
                var saldofinal = cuenta.saldo - Decimal.Parse(modelo.Monto);

                if (cuenta == null)
                {
                    ModelState.AddModelError("", "El usuario no tiene una cuenta vinculada");
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    cuenta.saldo = saldofinal;
                    
                    if (verificarInserciónServicio(modelo.Tipo, modelo.Cuenta, modelo.Monto, modelo.Descripcion, cliente))
                    {
                        ModelState.AddModelError("", "Registro realizado");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Registro no realizado");
                    }

                }

               

                /*string query = "UPDATE CUENTA SET saldo = " + saldofinal + " WHERE cod_cliente = @p0";
                await db.CUENTA.SqlQuery(query,cliente).SingleOrDefaultAsync();*/
            }
            else
            {
                ModelState.AddModelError("", "La cuenta no tiene fondos suficiente");
            }

            return View(modelo);
        }

        public bool verificarInserciónServicio(string tipo, string cuenta, string monto, string descripcion, int cliente)
        {

            _db.OPERACION.Add(new OPERACION
            {
                tipo = tipo,
                no_cuenta = cuenta,
                monto = Decimal.Parse(monto),
                descripcion = descripcion,
                cod_usuario = cliente
            });

            var status = _db.SaveChanges();

            if (status != 0)
            {
                return true;
            }
            return false;
        }


        // GET: Transferencia
        public ActionResult Transferencia()
        {
            if (Session["codigo_usuario"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Transferencia(Operacion modelo)
        {
            var cliente = Int32.Parse(Session["codigo_cliente"].ToString());

            // agregarTransferencias();
            if (verificarSaldoTransferencia(cliente, modelo.Monto))
            {
          
                var cuenta = _db.CUENTA.Where(x => x.cod_cliente == cliente).FirstOrDefault();
                var saldofinal = cuenta.saldo - Decimal.Parse(modelo.Monto);

                if (cuenta == null)
                {
                    ModelState.AddModelError("", "El usuario no tiene una cuenta vinculada");
                    return RedirectToAction("Index", "Home");
                }
                else
                {   
                    
                    cuenta.saldo = saldofinal;
                    //correcta inserción
                    if(verificarInserciónTransferencia(modelo.Cuenta, modelo.Monto, modelo.Descripcion, Session["codigo_usuario"].ToString()))
                    {
                        ModelState.AddModelError("", "Registro realizado");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Registro no realizado");
                    }
                }
                

            }
            else
            {
                ModelState.AddModelError("", "La cuenta no tiene fondos suficiente");
            }


            return View(modelo);
        }

        public bool verificarInserciónTransferencia(string cuenta, string monto, string descripcion, string cod_usuario)
        {

            _db.OPERACION.Add(new OPERACION
            {
                tipo = "TRANSFERENCIA",
                no_cuenta = cuenta,
                monto = Decimal.Parse(monto),
                descripcion = descripcion,
                cod_usuario = Int32.Parse(cod_usuario)
            });

            var status = _db.SaveChanges();

            if (status != 0)
            {
                return true;
            }
            return false;
        }

        public bool verificarSaldoTransferencia(int cliente, string monto)
        {
            var cuenta = _db.CUENTA.Where(x => x.cod_cliente == cliente).FirstOrDefault();

            if (cuenta.saldo >= Decimal.Parse(monto))
            {
                return true;
            }
            return false;
        }

        public ActionResult Consulta(Consulta modelo)
        {
            if (Session["codigo_usuario"] == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var cliente = Int32.Parse(Session["codigo_cliente"].ToString());
            var cuenta = _db.CUENTA.Where(x => x.cod_cliente == cliente).FirstOrDefault();
            string saldo = "";

            saldo = devuelveSaldo(cliente, saldo);

            if (saldo == "")
            {
                ModelState.AddModelError("", "El usuario no tiene saldo vinculado");
            }
            else
            {
                modelo.saldo = saldo;
            }

            return View(modelo);
        }
        

        public string devuelveSaldo(int cliente, string saldo)
        {
            saldo = "";
            var cuenta = _db.CUENTA.Where(x => x.cod_cliente == cliente).FirstOrDefault();

            if (cuenta == null)
            {
                ModelState.AddModelError("", "El usuario no tiene una cuenta vinculada");
                return saldo;
            }
            else
            {
                if(saldo != null)
                {
                    saldo = cuenta.saldo.ToString();
                    Console.WriteLine("Hola");
                    Trace.Write("Hola");
                }
                
                return saldo;
            }

        }


        // GET: Credito
        public ActionResult Credito()
        {
            if (Session["codigo_usuario"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Credito(Operacion modelo)
        {
            var cliente = Int32.Parse(Session["codigo_cliente"].ToString());
            var cuentaCredito = _db.CUENTA.Where(x => x.no_cuenta == modelo.Cuenta).FirstOrDefault();

            if (!verificarCuentaCredito(modelo.Cuenta))
            {
                ModelState.AddModelError("", "No existe la cuenta a acreditar");
                return View();
            }
            else
            {
                if (verificarSaldoCuentaCredito(cliente, modelo.Monto))
                {
                    //inserta operacion realizada
                    _db.OPERACION.Add(new OPERACION
                    {
                        tipo = "CRÉDITO",
                        no_cuenta = modelo.Cuenta,
                        monto = Decimal.Parse(modelo.Monto),
                        descripcion = modelo.Descripcion,
                        cod_usuario = Int32.Parse(Session["codigo_usuario"].ToString())
                    });

                    var cuenta = _db.CUENTA.Where(x => x.cod_cliente == cliente).FirstOrDefault();
                    var saldofinal = cuenta.saldo - Decimal.Parse(modelo.Monto);
                    var saldofinalCredito = cuentaCredito.saldo + Decimal.Parse(modelo.Monto);

                    if (cuenta == null)
                    {
                        ModelState.AddModelError("", "El usuario no tiene una cuenta vinculada");
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        //debita a cuenta
                        cuenta.saldo = saldofinal;
                        //credito a cuenta
                        cuentaCredito.saldo = saldofinalCredito;
                        _db.SaveChanges();
                        ModelState.AddModelError("", "Registro realizado");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "La cuenta no tiene fondos suficiente");
                }

            }

            return View(modelo);
        }

        public bool verificarCuentaCredito(string cuenta)
        {
            var cuentaDebito = _db.CUENTA.Where(x => x.no_cuenta == cuenta).FirstOrDefault();

            if (cuentaDebito != null)
            {
                return true;
            }
            return false;
        }

        public bool verificarSaldoCuentaCredito(int cliente, string monto)
        {
            var cuenta = _db.CUENTA.Where(x => x.cod_cliente == cliente).FirstOrDefault();

            if (cuenta.saldo >= Decimal.Parse(monto))
            {
                return true;
            }
            return false;
        }

        // GET: Debito
        public ActionResult Debito()
        {
            if (Session["codigo_usuario"] == null)
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }

        [HttpPost]
        public ActionResult Debito(Operacion modelo)
        {
            var cliente = Int32.Parse(Session["codigo_cliente"].ToString());
            var cuentaDebito = _db.CUENTA.Where(x => x.no_cuenta == modelo.Cuenta).FirstOrDefault();

            if (!verificarCuentaDebito(modelo.Cuenta))
            {
                ModelState.AddModelError("", "No existe la cuenta a debitar");
                return View();
            }
            else
            {
                if (verificarSaldoCuentaDebito(cuentaDebito.cod_cliente, modelo.Monto))
                {
                    _db.OPERACION.Add(new OPERACION
                    {
                        tipo = "DÉBITO",
                        //no_cuenta = Session["no_cuenta"].ToString(),
                        no_cuenta = modelo.Cuenta,
                        monto = Decimal.Parse(modelo.Monto),
                        descripcion = modelo.Descripcion,
                        cod_usuario = Int32.Parse(Session["codigo_usuario"].ToString())
                    });

                    var cuenta = _db.CUENTA.Where(x => x.cod_cliente == cliente).FirstOrDefault();
                    var saldofinal = cuenta.saldo + Decimal.Parse(modelo.Monto);
                    var saldofinalDebito = cuentaDebito.saldo - Decimal.Parse(modelo.Monto);

                    if (cuenta == null)
                    {
                        ModelState.AddModelError("", "El usuario no tiene una cuenta vinculada");
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        //Debita
                        cuentaDebito.saldo = saldofinalDebito;
                        //Acredita
                        cuenta.saldo = saldofinal;
                        _db.SaveChanges();
                        ModelState.AddModelError("", "Registro realizado");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "La cuenta no tiene fondos suficiente");
                }
            }
            return View(modelo);
        }

        public bool verificarCuentaDebito(string cuenta)
        {
            var cuentaDebito = _db.CUENTA.Where(x => x.no_cuenta == cuenta).FirstOrDefault();

            if (cuentaDebito != null)
            {
                return true;
            }
            return false;
        }

        public bool verificarSaldoCuentaDebito(int cliente, string monto)
        {
            var cuenta = _db.CUENTA.Where(x => x.cod_cliente == cliente).FirstOrDefault();

            if (cuenta.saldo >= Decimal.Parse(monto))
            {
                return true;
            }
            return false;
        }

        // GET: Operaciones
        public ActionResult Operaciones()
        {
            if (Session["codigo_usuario"] != null)
            {
                var cod_usuario = Int32.Parse(Session["codigo_usuario"].ToString());
                return View(_db.OPERACION.ToList().Where(x => x.cod_usuario == cod_usuario));
            }

            return View(_db.OPERACION.ToList());
        }

        // GET: Operaciones
        public ActionResult Logout()
        {
            if (Session["codigo_usuario"] != null)
            {
                var codigoUsuario = (int)Session["codigo_usuario"];
                var usuario = _db.USUARIO.Where(x => x.cod_usuario == codigoUsuario).FirstOrDefault();

                if (usuario != null)
                {
                    usuario.estado = "0";
                    _db.SaveChanges();

                    Session.Abandon();
                }
            }

            return RedirectToAction("Login", "Account");
        }

        public bool existeSaldo(int cliente, string monto)
        {
            var cuenta = _db.CUENTA.Where(x => x.cod_cliente == cliente).FirstOrDefault();

            if (cuenta.saldo >= Decimal.Parse(monto))
            {
                return true;
            }
            return false;
        }

        public bool agregaTransferencia(int cliente, string monto)
        {
            var cuenta = _db.CUENTA.Where(x => x.cod_cliente == cliente).FirstOrDefault();

            if (cuenta.saldo >= Decimal.Parse(monto))
            {
                return true;
            }
            return false;
            
        }

    }
}