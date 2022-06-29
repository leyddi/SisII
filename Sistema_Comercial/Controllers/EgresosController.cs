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
        [HttpPost]
        public ActionResult Index(DetalleCaja detalleCaja)
        {
            Usuario usuario = (Usuario)@Session["User"];
            ViewBag.NombreUsuario = usuario.Nombres;
            ViewBag.Rol = usuario.IdRol;
            int response = 0;
            ConnectionStringSettings cadenaDataBase = ConfigurationManager.ConnectionStrings["SistemaConnection"];
            using (SqlConnection connection = new SqlConnection(cadenaDataBase.ConnectionString))
            {

                SqlCommand sqlCommand = new SqlCommand("uspRegistrarEgreso", connection);
                sqlCommand.Parameters.AddWithValue("@PI_MONTO", (detalleCaja.MONTO>0? (detalleCaja.MONTO*(-1)): detalleCaja.MONTO));
                sqlCommand.Parameters.AddWithValue("@PI_ID_MONEDA", detalleCaja.ID_MONEDA);
                sqlCommand.Parameters.AddWithValue("@PI_MONTO_BS", detalleCaja.TIPO_CAMBIO* (detalleCaja.MONTO > 0 ? (detalleCaja.MONTO * (-1)) : detalleCaja.MONTO));
                sqlCommand.Parameters.AddWithValue("@PI_TIPO_CAMBIO", detalleCaja.TIPO_CAMBIO==0?1: detalleCaja.TIPO_CAMBIO);
                sqlCommand.Parameters.AddWithValue("@PI_TIPO", detalleCaja.TIPO);
                sqlCommand.Parameters.Add("@PI_ID", SqlDbType.Int).Direction = ParameterDirection.Output;
                sqlCommand.CommandType = CommandType.StoredProcedure;

                connection.Open();
                sqlCommand.ExecuteNonQuery();

                response = Convert.ToInt32(sqlCommand.Parameters["@PI_ID"].Value);
                connection.Close();
            }

            if (response == -1)
            {
                TempData["SuccessMessage"] = "Error al registrar al Egreso";
            }
            if (response == 0)
            {
                TempData["SuccessMessage"] = "Ocurrió un error al intentar registrar el Egreso";
            }
            if (response > 0)
            {
                TempData["SuccessMessage"] = "Registro exitoso";
                return View(new DetalleCaja());
            }
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