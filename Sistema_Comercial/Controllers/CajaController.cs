using Sistema_Comercial.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sistema_Comercial.Controllers
{
    public class CajaController : Controller
    {
        // GET: Caja
        public ActionResult Index()
        {
            Usuario usuario = (Usuario)@Session["User"];
            ViewBag.NombreUsuario = usuario.Nombres;
            ViewBag.Rol = usuario.IdRol;


            //LLAMAS A UN SP QUE PREGUNTE SI HAY UNA CAJA ABIERTA


            //if (RESULTADO == 1)
            //{
            return View();


            //}
            //else {

            //    return PartialView("~/Views/Caja/Cierre.cshtml");

            //}

        }

        public ActionResult Cierre() {
            Usuario usuario = (Usuario)@Session["User"];
            ViewBag.NombreUsuario = usuario.Nombres;
            ViewBag.Rol = usuario.IdRol;
            List<Caja> listCaja = new List<Caja>();

            return View(listCaja);
        }
        [HttpPost]
        public ActionResult Cierre(Caja caja)
        {
            Usuario usuario = (Usuario)@Session["User"];
            ViewBag.NombreUsuario = usuario.Nombres;
            ViewBag.Rol = usuario.IdRol;

            return PartialView("~/Views/Caja/Index.cshtml");
        }
        [HttpGet]
        public ActionResult GetMonedas()
        {
            List<Moneda> monedas = new List<Moneda>();
            ConnectionStringSettings cadenaDataBase = ConfigurationManager.ConnectionStrings["SistemaConnection"];

            using (SqlConnection connection = new SqlConnection(cadenaDataBase.ConnectionString))
            {

                SqlCommand sqlCommand = new SqlCommand("uspObtenerMonedas", connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                connection.Open();
                SqlDataReader objReader = sqlCommand.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        Moneda moneda = new Moneda();
                        moneda.DESCRIPCION = objReader.GetString(objReader.GetOrdinal("DESCRIPCION"));
                        moneda.ID = objReader.GetInt32(objReader.GetOrdinal("ID"));
                        monedas.Add(moneda);
                    }
                }
                connection.Close();
            }

            return Json(monedas, JsonRequestBehavior.AllowGet);
        }
    }
}