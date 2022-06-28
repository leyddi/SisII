using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sistema_Comercial.Models
{
    public class Usuario 
    {
       public int Id { get; set; }
        [Display(Name = "Usuario")]

        public string NombreUsuario { get; set; }
        [Display(Name = "Contraseña")]

        public string Contrasena { get; set; }
       public string Nombres { get; set; }
       public string Apellidos { get; set; }
       public DateTime FechaRegistro { get; set; }
       public int IdRol { get; set; }
       public string Rol { get; set; }


    }
}