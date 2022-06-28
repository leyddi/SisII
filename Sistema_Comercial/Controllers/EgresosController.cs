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
    public class EgresosController : Controller
    {
        // GET: Egresos
        public ActionResult Index()
        {
            Usuario usuario = (Usuario)@Session["User"];
            ViewBag.NombreUsuario = usuario.Nombres;
            ViewBag.Rol = usuario.IdRol;
            return View();
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