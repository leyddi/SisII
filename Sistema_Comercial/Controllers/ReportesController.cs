using PagedList;
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
        public ActionResult Ventas(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
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
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(ventas.ToPagedList(pageNumber, pageSize));
        }
        // GET: Reportes
        public ViewResult Pagos(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            Usuario usuario = (Usuario)@Session["User"];
            ViewBag.NombreUsuario = usuario.Nombres;
            ViewBag.Rol = usuario.IdRol;
            List<Ventas> ventas = new List<Ventas>();

            ConnectionStringSettings cadenaDataBase = ConfigurationManager.ConnectionStrings["SistemaConnection"];

            using (SqlConnection connection = new SqlConnection(cadenaDataBase.ConnectionString))
            {

                SqlCommand sqlCommand = new SqlCommand("uspListarVentasPagos", connection);
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
                        venta.PAGO = new Pago();
                        venta.PAGO.DETALLEPAGOS = new List<DetallePago>();
                        DetallePago detallePago = new DetallePago();
                        venta.ID = objReader.GetInt32(objReader.GetOrdinal("ID"));
                        venta.FECHAREGISTRO = objReader.GetDateTime(objReader.GetOrdinal("FECHAREGISTRO"));
                        venta.COMPROBANTE = objReader.GetString(objReader.GetOrdinal("COMPROBANTE"));
                        venta.TOTAL = objReader.GetDecimal(objReader.GetOrdinal("TOTAL"));
                        venta.CLIENTE.NOMBRES = objReader.GetString(objReader.GetOrdinal("CLIENTE"));
                        venta.USUARIO.Nombres = objReader.GetString(objReader.GetOrdinal("VENDEDOR"));
                        detallePago.DETALLECAJA = new DetalleCaja();
                        detallePago.DETALLECAJA.MONTO = objReader.GetDecimal(objReader.GetOrdinal("MONTO"));
                        detallePago.DETALLECAJA.MONEDA = objReader.GetString(objReader.GetOrdinal("DESCRIPCION"));
                        detallePago.DETALLECAJA.TIPO_CAMBIO = objReader.GetDecimal(objReader.GetOrdinal("TIPO_CAMBIO"));
                        detallePago.DETALLECAJA.MONTO_BS = objReader.GetDecimal(objReader.GetOrdinal("MONTO_BS"));
                        detallePago.DETALLECAJA.TIPO = objReader.GetString(objReader.GetOrdinal("TIPO"));

                        venta.PAGO.DETALLEPAGOS.Add(detallePago);

                        ventas.Add(venta);
                    }
                }
                connection.Close();
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(ventas.ToPagedList(pageNumber, pageSize));
        }
        // GET: Reportes
        public ActionResult DetalleVentas(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

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
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(ventas.ToPagedList(pageNumber, pageSize));
        }


    }
}
