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
    public class SistemaController : Controller
    {
        // GET: Sistema
        public ActionResult Usuario()
        {
            Usuario usuario = (Usuario)@Session["User"];
            ViewBag.NombreUsuario = usuario.Nombres;
            ViewBag.Rol = usuario.IdRol;
            return View();
        }
        [HttpPost]
        public ActionResult Usuario(Usuario usuario)
        {
            ConnectionStringSettings cadenaDataBase = ConfigurationManager.ConnectionStrings["SistemaConnection"];

            using (SqlConnection connection = new SqlConnection(cadenaDataBase.ConnectionString))
            {

                SqlCommand sqlCommand = new SqlCommand("uspRegistrarUsuario", connection);
                sqlCommand.Parameters.AddWithValue("@PI_USUARIO", usuario.NombreUsuario);
                sqlCommand.Parameters.AddWithValue("@PI_CONTRASENA", usuario.Contrasena);
                sqlCommand.Parameters.AddWithValue("@PI_NOMBRES", usuario.Nombres);
                sqlCommand.Parameters.AddWithValue("@PI_APELLIDOS", usuario.Apellidos);
                sqlCommand.Parameters.AddWithValue("@PI_ID_ROL", usuario.IdRol);
                sqlCommand.Parameters.Add("@PI_ID", SqlDbType.Int).Direction = ParameterDirection.Output;
                sqlCommand.CommandType = CommandType.StoredProcedure;

                connection.Open();
                sqlCommand.ExecuteNonQuery();

                usuario.Id = Convert.ToInt32(sqlCommand.Parameters["@PI_ID"].Value);
                connection.Close();
            }
            Usuario usuario2 = (Usuario)@Session["User"];
            ViewBag.NombreUsuario = usuario2.Nombres;
            ViewBag.Rol = usuario2.IdRol;
            if (usuario.Id == -1)
            {
                TempData["SuccessMessage"] = "Error al registrar al usuario";
            }
            if (usuario.Id == 0)
            {
                TempData["SuccessMessage"] = "Ocurrió un error al intentar registrar el usuario";
            }
            if (usuario.Id > 0)
            {
                TempData["SuccessMessage"] = "Registro exitoso";
                return View(new Usuario());
            }
            
            return View();
        }

        [HttpGet]
        public ActionResult GetRoles()
        {
            List<Rol> roles = new List<Rol>();
            ConnectionStringSettings cadenaDataBase = ConfigurationManager.ConnectionStrings["SistemaConnection"];

            using (SqlConnection connection = new SqlConnection(cadenaDataBase.ConnectionString))
            {

                SqlCommand sqlCommand = new SqlCommand("uspListarRoles", connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                connection.Open();
                SqlDataReader objReader = sqlCommand.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        Rol rol = new Rol();
                        rol.DESCRIPCION = objReader.GetString(objReader.GetOrdinal("DESCRIPCION"));
                        rol.ID = objReader.GetInt32(objReader.GetOrdinal("ID"));
                        roles.Add(rol);
                    }
                }
                connection.Close();
            }

            return Json(roles, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult List()
        {
            List<Usuario> usuarios = new List<Usuario>();

            ConnectionStringSettings cadenaDataBase = ConfigurationManager.ConnectionStrings["SistemaConnection"];

            using (SqlConnection connection = new SqlConnection(cadenaDataBase.ConnectionString))
            {

                SqlCommand sqlCommand = new SqlCommand("uspListarUsuario", connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                connection.Open();
                SqlDataReader objReader = sqlCommand.ExecuteReader();
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        Usuario usuario = new Usuario();
                        usuario.NombreUsuario = objReader.GetString(objReader.GetOrdinal("NombreUsuario"));
                        usuario.Contrasena = objReader.GetString(objReader.GetOrdinal("Constrasena"));
                        usuario.Nombres = objReader.GetString(objReader.GetOrdinal("Nombres"));
                        usuario.Apellidos = objReader.GetString(objReader.GetOrdinal("Apellidos"));
                        usuario.FechaRegistro = objReader.GetDateTime(objReader.GetOrdinal("FechaRegistro"));
                        usuario.Id = objReader.GetInt32(objReader.GetOrdinal("ID"));
                        usuario.IdRol = objReader.GetInt32(objReader.GetOrdinal("Id_Rol"));
                        usuario.Rol = objReader.GetString(objReader.GetOrdinal("Rol")); 
                        usuarios.Add(usuario);
                    }
                }
                connection.Close();
            }
            return PartialView("~/Views/Sistema/List.cshtml", usuarios);
        }

        public ActionResult Eliminar(int ID)
        {
            ConnectionStringSettings cadenaDataBase = ConfigurationManager.ConnectionStrings["SistemaConnection"];
            Usuario usuario = new Usuario();
            using (SqlConnection connection = new SqlConnection(cadenaDataBase.ConnectionString))
            {

                SqlCommand sqlCommand = new SqlCommand("uspObtenerUsuarioID", connection);
                sqlCommand.Parameters.AddWithValue("@ID", ID);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                connection.Open();
                SqlDataReader rdr = sqlCommand.ExecuteReader();

                while (rdr.Read())
                {

                    usuario.Id = Convert.ToInt32(rdr["Id"].ToString());
                    usuario.NombreUsuario = rdr.GetString(rdr.GetOrdinal("NombreUsuario"));
                    usuario.Nombres = rdr.GetString(rdr.GetOrdinal("Nombres"));
                    usuario.Apellidos = rdr.GetString(rdr.GetOrdinal("Apellidos"));
                    usuario.FechaRegistro = rdr.GetDateTime(rdr.GetOrdinal("FechaRegistro"));
                    //usuario.IdRol = objReader.GetInt32(objReader.GetOrdinal("IdRol"));
                    //usuario.Rol = rdr.GetString(rdr.GetOrdinal("Rol"));

                }
                connection.Close();
            }
            ViewBag.showSuccessAlert = false;
            return View(usuario);
        }
        [HttpPost]
        public ActionResult Eliminar(Usuario usuario)
        {


            ConnectionStringSettings cadenaDataBase = ConfigurationManager.ConnectionStrings["SistemaConnection"];

            using (SqlConnection connection = new SqlConnection(cadenaDataBase.ConnectionString))
            {

                SqlCommand sqlCommand = new SqlCommand("uspEliminarUsuario", connection);
                sqlCommand.Parameters.AddWithValue("@ID", usuario.Id);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                connection.Open();
                sqlCommand.ExecuteNonQuery();

                //productos.ID = Convert.ToInt32(sqlCommand.Parameters["@PI_OUT"].Value);
                connection.Close();
            }

            if (usuario.Id == -1)
            {
                ViewData["Mensaje"] = "El código ya se encuentra asignado a otro producto";
            }
            if (usuario.Id == 0)
            {
                ViewData["Mensaje"] = "Ocurrió un error al intentar registrar el producto";
            }
            if (usuario.Id > 0)
            {
                ViewData["Mensaje"] = "Registro exitoso";
            }
            ViewBag.showSuccessAlert = true;
            return RedirectToAction("Usuario");
        }
    }
}