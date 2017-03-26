using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AyD_P2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        // GET: Servicio
        public ActionResult Account()
        {
            return View();
        }

        // GET: Transferencia
        public ActionResult Contact()
        {
            return View();
        }
        
    }
}