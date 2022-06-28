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
    }
}