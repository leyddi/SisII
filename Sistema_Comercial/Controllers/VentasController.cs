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
    public class VentasController : Controller
    {
        // GET: Ventas

        public ActionResult Index()
        {
            Usuario usuario = (Usuario)@Session["User"];
            Ventas ventas = new Ventas();
            ViewBag.NombreUsuario = usuario.Nombres;
            ViewBag.Rol = usuario.IdRol;
            ventas.PRODUCTOS = new List<Productos>();
            ventas.PRODUCTOS.Add(new Productos { ID = 0 });          
            
            return View(ventas);
        }
        [HttpGet]
        public ActionResult AddRow()
        {
            Productos productos = new Productos();
            return PartialView("~/Views/Ventas/Row.cshtml", productos);
        }
        [HttpGet]
        public ActionResult GetProduct(string code)
        {
            Productos producto = new Productos();
            ConnectionStringSettings cadenaDataBase = ConfigurationManager.ConnectionStrings["SistemaConnection"];

            using (SqlConnection connection = new SqlConnection(cadenaDataBase.ConnectionString))
            {

                SqlCommand sqlCommand = new SqlCommand("uspObtenerProducto", connection);
                sqlCommand.Parameters.AddWithValue("@PI_CODIGO", code);               
                sqlCommand.CommandType = CommandType.StoredProcedure;

                connection.Open();
                SqlDataReader objReader = sqlCommand.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        producto.CODIGO = objReader.GetString(objReader.GetOrdinal("CODIGO"));
                        producto.DESCRIPCION = objReader.GetString(objReader.GetOrdinal("DESCRIPCION"));
                        producto.MARCA = objReader["MARCA"] == System.DBNull.Value ? "" : objReader.GetString(objReader.GetOrdinal("MARCA"));
                        producto.MODELO = objReader["MODELO"] == System.DBNull.Value ? "" : objReader.GetString(objReader.GetOrdinal("MODELO"));
                        producto.PVP = objReader.GetDecimal(objReader.GetOrdinal("PVP"));
                        producto.CANTIDAD = objReader.GetDecimal(objReader.GetOrdinal("CANTIDAD"));
                        producto.ID = objReader.GetInt32(objReader.GetOrdinal("ID"));
                        producto.FECHAREGISTRO = objReader.GetDateTime(objReader.GetOrdinal("FECHAREGISTRO"));
                        break;
                    }
                }
                connection.Close();
            }

            return Json(producto, JsonRequestBehavior.AllowGet);
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