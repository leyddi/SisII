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

        [Required(ErrorMessage = "Debe ingresar un Usuario")]
        [Display(Name = "Usuario")]
        public string NombreUsuario { get; set; }

        [Required(ErrorMessage = "Debe ingresar una Contraseña")]
        [DataType(DataType.Password)]
        [StringLength(5, ErrorMessage = "La contraseña debe tener al menos 5 caracteres")]
        [Display(Name = "Contraseña")]
        public string Contrasena { get; set; }

        [Required(ErrorMessage = "Debe ingresar al menos un Nombre")]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "Debe ingresar al menos un Apellido")]
        public string Apellidos { get; set; }
       
        [Display(Name = "Fecha de Registro")]
        public DateTime FechaRegistro { get; set; }
        
        public int IdRol { get; set; }
       
        public string Rol { get; set; }


    }
}