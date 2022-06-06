using Sistema_Comercial.Models;
using Sistema_Comercial.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlelaProject.Controllers
{
    [ValidateSesion]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Usuario usuario = (Usuario)@Session["User"];
            ViewBag.NombreUsuario = usuario.Nombres;
            ViewBag.Rol = usuario.IdRol;
            return View(usuario);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Login", "Auth");
        }
    }
}