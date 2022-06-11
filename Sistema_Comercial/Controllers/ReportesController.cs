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
    public class ReportesController : Controller
    {
        // GET: Reportes
        public ActionResult Ventas()
        {
            Usuario usuario = (Usuario)@Session["User"];
            ViewBag.NombreUsuario = usuario.Nombres;
            ViewBag.Rol = usuario.IdRol;
            List<Ventas> ventas = new List<Ventas>();

            ConnectionStringSettings cadenaDataBase = ConfigurationManager.ConnectionStrings["SistemaConnection"];

            using (SqlConnection connection = new SqlConnection(cadenaDataBase.ConnectionString))
            {

                SqlCommand sqlCommand = new SqlCommand("uspListarVentas", connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                connection.Open();
                SqlDataReader objReader = sqlCommand.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        Ventas venta = new Ventas();
                        venta.CLIENTE = new Cliente();
                        venta.USUARIO = new Usuario();
                        venta.ID = objReader.GetInt32(objReader.GetOrdinal("ID"));
                        venta.FECHAREGISTRO = objReader.GetDateTime(objReader.GetOrdinal("FECHAREGISTRO"));
                        venta.COMPROBANTE = objReader.GetString(objReader.GetOrdinal("COMPROBANTE"));
                        venta.TOTAL = objReader.GetDecimal(objReader.GetOrdinal("TOTAL"));
                        venta.CLIENTE.NOMBRES = objReader.GetString(objReader.GetOrdinal("CLIENTE"));
                        venta.USUARIO.Nombres = objReader.GetString(objReader.GetOrdinal("USUARIO"));


                        ventas.Add(venta);
                    }
                }
                connection.Close();
            }
            return View(ventas);
        }
        // GET: Reportes
        public ActionResult Pagos()
        {
            return View();
        }
        // GET: Reportes
        public ActionResult DetalleVentas()
        {
            Usuario usuario = (Usuario)@Session["User"];
            ViewBag.NombreUsuario = usuario.Nombres;
            ViewBag.Rol = usuario.IdRol;
            List<Ventas> ventas = new List<Ventas>();

            ConnectionStringSettings cadenaDataBase = ConfigurationManager.ConnectionStrings["SistemaConnection"];

            using (SqlConnection connection = new SqlConnection(cadenaDataBase.ConnectionString))
            {

                SqlCommand sqlCommand = new SqlCommand("uspListarVentasDetalle", connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                connection.Open();
                SqlDataReader objReader = sqlCommand.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        Ventas venta = new Ventas();
                        venta.CLIENTE = new Cliente();
                        venta.USUARIO = new Usuario();
                        venta.PRODUCTOS = new List<Productos>();
                        Productos productos = new Productos();
                        venta.ID = objReader.GetInt32(objReader.GetOrdinal("ID"));
                        venta.FECHAREGISTRO = objReader.GetDateTime(objReader.GetOrdinal("FECHAREGISTRO"));
                        venta.COMPROBANTE = objReader.GetString(objReader.GetOrdinal("COMPROBANTE"));
                        venta.TOTAL = objReader.GetDecimal(objReader.GetOrdinal("TOTAL"));
                        venta.CLIENTE.NOMBRES = objReader.GetString(objReader.GetOrdinal("CLIENTE"));
                        venta.USUARIO.Nombres = objReader.GetString(objReader.GetOrdinal("VENDEDOR"));
                        productos.CANTIDAD = objReader.GetDecimal(objReader.GetOrdinal("CANTIDAD"));
                        productos.PVP = objReader.GetDecimal(objReader.GetOrdinal("PVP"));
                        productos.TOTAL = objReader.GetDecimal(objReader.GetOrdinal("PVP_TOTAL"));
                        productos.DESCRIPCION = objReader.GetString(objReader.GetOrdinal("DESCRIPCION"));
                        venta.PRODUCTOS.Add(productos);

                        ventas.Add(venta);
                    }
                }
                connection.Close();
            }
            return View(ventas);
        }


    }
}
