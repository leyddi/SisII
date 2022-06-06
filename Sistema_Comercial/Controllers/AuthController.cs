using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sistema_Comercial.Models;
namespace Sistema_Comercial.Controllers
{
    public class AuthController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Usuario usuario)
        {
            ConnectionStringSettings cadenaDataBase = ConfigurationManager.ConnectionStrings["SistemaConnection"];

            using (SqlConnection connection = new SqlConnection(cadenaDataBase.ConnectionString)) {

                SqlCommand sqlCommand = new SqlCommand("uspValidarCredencialesLogin", connection);
                sqlCommand.Parameters.AddWithValue("@PI_Usuario", usuario.NombreUsuario);
                sqlCommand.Parameters.AddWithValue("@PI_Contrasena", usuario.Contrasena);
                sqlCommand.Parameters.Add("@PI_IdUser", SqlDbType.Int).Direction = ParameterDirection.Output;
                sqlCommand.CommandType = CommandType.StoredProcedure;

                connection.Open();
                sqlCommand.ExecuteNonQuery();

                usuario.Id = Convert.ToInt32(sqlCommand.Parameters["@PI_IdUser"].Value);
                connection.Close();
            }

            if (usuario.Id != 0)
            {
                Session["User"] = usuario;

                using (SqlConnection connection = new SqlConnection(cadenaDataBase.ConnectionString))
                {

                    SqlCommand sqlCommand = new SqlCommand("uspObtenerUsuario", connection);
                    sqlCommand.Parameters.AddWithValue("@PI_Id", usuario.Id);
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    connection.Open();
                    SqlDataReader rdr = sqlCommand.ExecuteReader();

                    while (rdr.Read())
                    {
                        usuario.Nombres = rdr["NOMBRES"].ToString();
                        usuario.Apellidos = rdr["APELLIDOS"].ToString();
                        usuario.FechaRegistro = Convert.ToDateTime(rdr["FECHAREGISTRO"].ToString());
                        usuario.IdRol = Convert.ToInt32(rdr["ID_ROL"].ToString());
                        usuario.Rol = rdr["ROL"].ToString();
                    }
                    connection.Close();
                }

                return RedirectToAction("Index", "Home");
            }
            else {
                ViewData["Mensaje"] = "Credenciales incorrectas";
                return View();

            }
        }
    }
}
