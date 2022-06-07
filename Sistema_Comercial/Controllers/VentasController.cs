using Sistema_Comercial.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sistema_Comercial.Controllers
{
    public class VentasController : Controller
    {
        // GET: Ventas

        public ActionResult Index()
        {
            Usuario usuario = (Usuario)@Session["User"];
            Ventas ventas = new Ventas();
            ventas.PRODUCTOS = new List<Productos>();
            ventas.PRODUCTOS.Add(new Productos { ID = 0 });
            
            ViewBag.NombreUsuario = usuario.Nombres;
            ViewBag.Rol = usuario.IdRol;
            return View(ventas);
        }
        [HttpGet]
        public ActionResult AddRow()
        {
            Productos productos = new Productos();
            return PartialView("~/Views/Ventas/Row.cshtml", productos);
        }
    }
}