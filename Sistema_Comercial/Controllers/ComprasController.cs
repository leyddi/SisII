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
    public class ComprasController : Controller
    {
        // GET: Compras
        public ActionResult Index()
        {
            Usuario usuario = (Usuario)@Session["User"];
            Compras compras = new Compras();
            ViewBag.NombreUsuario = usuario.Nombres;
            ViewBag.Rol = usuario.IdRol;
            compras.PRODUCTOS = new List<Productos>();
            compras.PRODUCTOS.Add(new Productos { ID = 0 });
            compras.PROVEEDOR = new Proveedor();

            return View(compras);
        }
        [HttpPost]
        public ActionResult Index(Compras compras)
        {
            Usuario usuario = (Usuario)@Session["User"];

            ConnectionStringSettings cadenaDataBase = ConfigurationManager.ConnectionStrings["SistemaConnection"];
            if (compras.PROVEEDOR.ID == 0 || compras.PROVEEDOR.ID == null)
            {
                using (SqlConnection connection = new SqlConnection(cadenaDataBase.ConnectionString))
                {

                    SqlCommand sqlCommand = new SqlCommand("uspRegistrarProveedor", connection);
                    sqlCommand.Parameters.AddWithValue("@PI_TIPO_DOCUMENTO", compras.PROVEEDOR.ID_TIPODOCUMENTO);
                    sqlCommand.Parameters.AddWithValue("@PI_NUMERO_DOCUMENTO", compras.PROVEEDOR.NUMERODOCUMENTO);
                    sqlCommand.Parameters.AddWithValue("@PI_DESCRIPCION", compras.PROVEEDOR.DESCRIPCION);
                    sqlCommand.Parameters.Add("@PI_ID_PROVEEDOR", SqlDbType.Int).Direction = ParameterDirection.Output;
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    connection.Open();
                    sqlCommand.ExecuteNonQuery();

                    compras.PROVEEDOR.ID = Convert.ToInt32(sqlCommand.Parameters["@PI_ID_PROVEEDOR"].Value);
                    connection.Close();
                }


                
            }


             using (SqlConnection connection = new SqlConnection(cadenaDataBase.ConnectionString))
            {

                SqlCommand sqlCommand = new SqlCommand("uspRegistrarCompra", connection);
                sqlCommand.Parameters.AddWithValue("@PI_MONTO", compras.TOTAL);
                sqlCommand.Parameters.AddWithValue("@PI_ID_USUARIO",usuario.Id);
                sqlCommand.Parameters.AddWithValue("@PI_ID_PROVEEDOR", compras.PROVEEDOR.ID);
                sqlCommand.Parameters.AddWithValue("@PI_COMPROBANTE", compras.SERIE);
                sqlCommand.Parameters.Add("@PO_ID", SqlDbType.Int).Direction = ParameterDirection.Output;
                sqlCommand.CommandType = CommandType.StoredProcedure;

                connection.Open();
                sqlCommand.ExecuteNonQuery();

                compras.ID = Convert.ToInt32(sqlCommand.Parameters["@PO_ID"].Value);
                connection.Close();
            }

            using (SqlConnection connection = new SqlConnection(cadenaDataBase.ConnectionString))
            {

                SqlCommand sqlCommand = new SqlCommand("uspRegistrarCompra", connection);
                sqlCommand.Parameters.AddWithValue("@PI_MONTO", compras.TOTAL);
                sqlCommand.Parameters.AddWithValue("@PI_ID_USUARIO", usuario.Id);
                sqlCommand.Parameters.AddWithValue("@PI_ID_PROVEEDOR", compras.PROVEEDOR.ID);
                sqlCommand.Parameters.AddWithValue("@PI_COMPROBANTE", compras.SERIE);
                sqlCommand.Parameters.Add("@PO_ID", SqlDbType.Int).Direction = ParameterDirection.Output;
                sqlCommand.CommandType = CommandType.StoredProcedure;

                connection.Open();
                sqlCommand.ExecuteNonQuery();

                compras.ID = Convert.ToInt32(sqlCommand.Parameters["@PO_ID"].Value);
                connection.Close();
            }

            foreach (Productos productos in compras.PRODUCTOS) {
                using (SqlConnection connection = new SqlConnection(cadenaDataBase.ConnectionString))
                {

                    SqlCommand sqlCommand = new SqlCommand("uspRegistrarDetalleCompra", connection);
                    sqlCommand.Parameters.AddWithValue("@PI_CANTIDAD", productos.CANTIDAD);
                    sqlCommand.Parameters.AddWithValue("@PI_PVP", productos.PVP);
                    sqlCommand.Parameters.AddWithValue("@PI_ID_PRODUCTO", productos.ID);
                    sqlCommand.Parameters.AddWithValue("@PI_ID_COMPRA", compras.ID);
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    connection.Open();
                    sqlCommand.ExecuteNonQuery();

                    connection.Close();
                }

            }
            compras = new Compras();
            compras.PRODUCTOS = new List<Productos>();
            compras.PRODUCTOS.Add(new Productos { ID = 0 });
            compras.PROVEEDOR = new Proveedor();
            return View(compras);
        }
                [HttpGet]
        public ActionResult AddRow()
        {
            Productos productos = new Productos();
            return PartialView("~/Views/Compras/Row.cshtml", productos);
        }
        [HttpGet]
        public ActionResult GetProveedor(string TipoDocumento, string NumeroDocumento)
        {
            Proveedor proveedor = new Proveedor();
            ConnectionStringSettings cadenaDataBase = ConfigurationManager.ConnectionStrings["SistemaConnection"];

            using (SqlConnection connection = new SqlConnection(cadenaDataBase.ConnectionString))
            {

                SqlCommand sqlCommand = new SqlCommand("uspObtenerProveedor", connection);
                sqlCommand.Parameters.AddWithValue("@PI_TIPO_DOCUMENTO", TipoDocumento);
                sqlCommand.Parameters.AddWithValue("@PI_NUMERO_DOCUMENTO", NumeroDocumento);

                sqlCommand.CommandType = CommandType.StoredProcedure;

                connection.Open();
                SqlDataReader objReader = sqlCommand.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        proveedor.DESCRIPCION = objReader.GetString(objReader.GetOrdinal("DESCRIPCION"));
                        proveedor.ID = objReader.GetInt32(objReader.GetOrdinal("ID"));
                        proveedor.ID_TIPODOCUMENTO = int.Parse(TipoDocumento);
                        proveedor.NUMERODOCUMENTO = NumeroDocumento;
                    }
                }
                connection.Close();
            }

            return Json(proveedor, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult SetProveedor(Proveedor proveedor)
        {
            ConnectionStringSettings cadenaDataBase = ConfigurationManager.ConnectionStrings["SistemaConnection"];

            using (SqlConnection connection = new SqlConnection(cadenaDataBase.ConnectionString))
            {

                SqlCommand sqlCommand = new SqlCommand("uspRegistrarProveedor", connection);
                sqlCommand.Parameters.AddWithValue("@PI_TIPO_DOCUMENTO", proveedor.ID_TIPODOCUMENTO);
                sqlCommand.Parameters.AddWithValue("@PI_NUMERO_DOCUMENTO", proveedor.NUMERODOCUMENTO);
                sqlCommand.Parameters.AddWithValue("@PI_DESCRIPCION", proveedor.DESCRIPCION);
                sqlCommand.Parameters.Add("@PI_ID_PROVEEDOR", SqlDbType.Int).Direction = ParameterDirection.Output;
                sqlCommand.CommandType = CommandType.StoredProcedure;

                connection.Open();
                sqlCommand.ExecuteNonQuery();

                proveedor.ID = Convert.ToInt32(sqlCommand.Parameters["@PI_ID_PROVEEDOR"].Value);
                connection.Close();
            }

            return Json(proveedor);
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
    }
}