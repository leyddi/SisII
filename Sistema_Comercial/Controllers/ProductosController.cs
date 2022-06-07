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
    public class ProductosController : Controller
    {
        // GET: Productos
        public ActionResult Index()
        {
            Usuario usuario = (Usuario)@Session["User"];
            ViewBag.NombreUsuario = usuario.Nombres;
            ViewBag.Rol = usuario.IdRol;
            return View();
        }

        [HttpPost]
        public ActionResult Index(Productos productos)
        {
            ConnectionStringSettings cadenaDataBase = ConfigurationManager.ConnectionStrings["SistemaConnection"];
            
            using (SqlConnection connection = new SqlConnection(cadenaDataBase.ConnectionString))
            {

                SqlCommand sqlCommand = new SqlCommand("uspRegistrarProducto", connection);
                sqlCommand.Parameters.AddWithValue("@PI_CODIGO", productos.CODIGO);
                sqlCommand.Parameters.AddWithValue("@PI_DESCRIPCION", productos.DESCRIPCION);
                sqlCommand.Parameters.AddWithValue("@PI_PVP", productos.PVP);
                sqlCommand.Parameters.AddWithValue("@PI_CANTIDAD", productos.CANTIDAD);
                sqlCommand.Parameters.AddWithValue("@PI_MARCA", productos.MARCA);
                sqlCommand.Parameters.AddWithValue("@PI_MODELO", productos.MODELO);
                sqlCommand.Parameters.Add("@PI_OUT", SqlDbType.Int).Direction = ParameterDirection.Output;
                sqlCommand.CommandType = CommandType.StoredProcedure;

                connection.Open();
                sqlCommand.ExecuteNonQuery();

                productos.ID = Convert.ToInt32(sqlCommand.Parameters["@PI_OUT"].Value);
                connection.Close();
            }

            if (productos.ID == -1) {
                ViewData["Mensaje"] = "El código ya se encuentra asignado a otro producto";
            }
            if (productos.ID == 0)
            {
                ViewData["Mensaje"] = "Ocurrió un error al intentar registrar el producto";
            }
            if (productos.ID > 0)
            {
                ViewData["Mensaje"] = "Registro exitoso";
            }
            return View();
        }

        public PartialViewResult List()
        {
            List<Productos> productos = new List<Productos>();

            ConnectionStringSettings cadenaDataBase = ConfigurationManager.ConnectionStrings["SistemaConnection"];

            using (SqlConnection connection = new SqlConnection(cadenaDataBase.ConnectionString))
            {

                SqlCommand sqlCommand = new SqlCommand("uspListarProductos", connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                connection.Open();
                SqlDataReader objReader = sqlCommand.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        Productos producto = new Productos();
                        producto.CODIGO = objReader.GetString(objReader.GetOrdinal("CODIGO"));
                        producto.DESCRIPCION = objReader.GetString(objReader.GetOrdinal("DESCRIPCION"));
                        producto.MARCA = objReader["MARCA"]==System.DBNull.Value?"":objReader.GetString(objReader.GetOrdinal("MARCA"));
                        producto.MODELO = objReader["MODELO"] == System.DBNull.Value ? "" : objReader.GetString(objReader.GetOrdinal("MODELO"));
                        producto.PVP = objReader.GetDecimal(objReader.GetOrdinal("PVP"));
                        producto.CANTIDAD = objReader.GetDecimal(objReader.GetOrdinal("CANTIDAD"));
                        producto.ID = objReader.GetInt32(objReader.GetOrdinal("ID"));
                        producto.FECHAREGISTRO = objReader.GetDateTime(objReader.GetOrdinal("FECHAREGISTRO"));


                        productos.Add(producto);
                    }
                }
                connection.Close();
            }
            return PartialView("~/Views/Productos/List.cshtml", productos);
        }
    }
}
