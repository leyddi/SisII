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
        [HttpPost]
        public ActionResult Index(Ventas ventas)
        {
            Usuario usuario = (Usuario)@Session["User"];

            ConnectionStringSettings cadenaDataBase = ConfigurationManager.ConnectionStrings["SistemaConnection"];
            if (ventas.CLIENTE.ID == 0 || ventas.CLIENTE.ID == null)
            {
                using (SqlConnection connection = new SqlConnection(cadenaDataBase.ConnectionString))
                {

                    SqlCommand sqlCommand = new SqlCommand("uspRegistrarCliente", connection);
                    sqlCommand.Parameters.AddWithValue("@PI_TIPO_DOCUMENTO", ventas.CLIENTE.ID_TIPODOCUMENTO);
                    sqlCommand.Parameters.AddWithValue("@PI_NUMERO_DOCUMENTO", ventas.CLIENTE.NUMERODOCUMENTO);
                    sqlCommand.Parameters.AddWithValue("@PI_NOMBRES", ventas.CLIENTE.NOMBRES);
                    sqlCommand.Parameters.Add("@PI_ID_CLIENTE", SqlDbType.Int).Direction = ParameterDirection.Output;
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    connection.Open();
                    sqlCommand.ExecuteNonQuery();

                    ventas.CLIENTE.ID = Convert.ToInt32(sqlCommand.Parameters["@PI_ID_CLIENTE"].Value);
                    connection.Close();
                }
            }

            using (SqlConnection connection = new SqlConnection(cadenaDataBase.ConnectionString))
            {

                SqlCommand sqlCommand = new SqlCommand("uspRegistrarVenta", connection);
                sqlCommand.Parameters.AddWithValue("@PI_MONTO", ventas.TOTAL);
                sqlCommand.Parameters.AddWithValue("@PI_ID_CLIENTE", ventas.CLIENTE.ID);
                sqlCommand.Parameters.AddWithValue("@PI_ID_USUARIO", usuario.Id);

                sqlCommand.CommandType = CommandType.StoredProcedure;

                connection.Open();
                SqlDataReader objReader = sqlCommand.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        ventas.ID = objReader.GetInt32(objReader.GetOrdinal("ID_VENTA"));
                        ventas.PAGO.ID = objReader.GetInt32(objReader.GetOrdinal("ID_PAGO"));
                        break;
                    }
                }
                connection.Close();
            }
            foreach (Productos producto in ventas.PRODUCTOS) {
                using (SqlConnection connection = new SqlConnection(cadenaDataBase.ConnectionString))
                {

                    SqlCommand sqlCommand = new SqlCommand("uspRegistrarDetalleVenta", connection);
                    sqlCommand.Parameters.AddWithValue("@PI_CANTIDAD", producto.CANTIDAD);
                    sqlCommand.Parameters.AddWithValue("@PI_ID_PRODUCTO", producto.ID);
                    sqlCommand.Parameters.AddWithValue("@PI_ID_VENTAS", ventas.ID);

                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    connection.Open();
                    sqlCommand.ExecuteNonQuery();                    
                    connection.Close();
                }
            }
            foreach (DetallePago detallePago in ventas.PAGO.DETALLEPAGOS)
            {
                using (SqlConnection connection = new SqlConnection(cadenaDataBase.ConnectionString))
                {

                    SqlCommand sqlCommand = new SqlCommand("uspRegistrarDetalleCaja", connection);
                    sqlCommand.Parameters.AddWithValue("@PI_MONTO", detallePago.DETALLECAJA.MONTO);
                    sqlCommand.Parameters.AddWithValue("@PI_MONTO_BS", detallePago.DETALLECAJA.MONTO_BS);
                    sqlCommand.Parameters.AddWithValue("@PI_TIPO_CAMBIO", detallePago.DETALLECAJA.TIPO_CAMBIO);
                    sqlCommand.Parameters.AddWithValue("@PI_ID_VENTA", ventas.ID);
                    sqlCommand.Parameters.AddWithValue("@PI_ID_MONEDA", detallePago.DETALLECAJA.ID_MONEDA);
                    sqlCommand.Parameters.AddWithValue("@PI_ID_PAGO", ventas.PAGO.ID);

                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    connection.Open();
                    sqlCommand.ExecuteNonQuery();
                    connection.Close();
                }
            }

            using (SqlConnection connection = new SqlConnection(cadenaDataBase.ConnectionString))
            {

                SqlCommand sqlCommand = new SqlCommand("uspRegistrarVuelto", connection);
                sqlCommand.Parameters.AddWithValue("@PI_ID_VENTA", ventas.ID);
                sqlCommand.Parameters.AddWithValue("@PI_ID_PAGO", ventas.PAGO.ID);

                sqlCommand.CommandType = CommandType.StoredProcedure;

                connection.Open();
                sqlCommand.ExecuteNonQuery();
                connection.Close();
            }

            usuario = (Usuario)@Session["User"];
             ventas = new Ventas();
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
        [HttpGet]
        public ActionResult GetCliente(string TipoDocumento, string NumeroDocumento)
        {
            Cliente cliente = new Cliente();
            ConnectionStringSettings cadenaDataBase = ConfigurationManager.ConnectionStrings["SistemaConnection"];

            using (SqlConnection connection = new SqlConnection(cadenaDataBase.ConnectionString))
            {

                SqlCommand sqlCommand = new SqlCommand("uspObtenerCliente", connection);
                sqlCommand.Parameters.AddWithValue("@PI_TIPO_DOCUMENTO", TipoDocumento);
                sqlCommand.Parameters.AddWithValue("@PI_NUMERO_DOCUMENTO", NumeroDocumento);

                sqlCommand.CommandType = CommandType.StoredProcedure;

                connection.Open();
                SqlDataReader objReader = sqlCommand.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        cliente.NOMBRES = objReader.GetString(objReader.GetOrdinal("NOMBRES"));
                        cliente.ID = objReader.GetInt32(objReader.GetOrdinal("ID"));
                        cliente.ID_TIPODOCUMENTO = int.Parse(TipoDocumento);
                        cliente.NUMERODOCUMENTO = NumeroDocumento;
                    }
                }
                connection.Close();
            }

            return Json(cliente, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SetCliente(Cliente cliente)
        {
            ConnectionStringSettings cadenaDataBase = ConfigurationManager.ConnectionStrings["SistemaConnection"];

            using (SqlConnection connection = new SqlConnection(cadenaDataBase.ConnectionString))
            {

                SqlCommand sqlCommand = new SqlCommand("uspRegistrarCliente", connection);
                sqlCommand.Parameters.AddWithValue("@PI_TIPO_DOCUMENTO", cliente.ID_TIPODOCUMENTO);
                sqlCommand.Parameters.AddWithValue("@PI_NUMERO_DOCUMENTO", cliente.NUMERODOCUMENTO);
                sqlCommand.Parameters.AddWithValue("@PI_NOMBRES", cliente.NOMBRES);
                sqlCommand.Parameters.Add("@PI_ID_CLIENTE", SqlDbType.Int).Direction = ParameterDirection.Output;
                sqlCommand.CommandType = CommandType.StoredProcedure;

                connection.Open();
                sqlCommand.ExecuteNonQuery();

                cliente.ID = Convert.ToInt32(sqlCommand.Parameters["@PI_ID_CLIENTE"].Value);
                connection.Close();
            }

            return Json(cliente);
        }
    }
}