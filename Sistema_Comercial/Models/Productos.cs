using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sistema_Comercial.Models
{
    public class Productos
    {
        public int ID { get; set; }
        [Required]
        public string CODIGO { get; set; }
        public string DESCRIPCION { get; set; }
        [DataType(DataType.Currency)]
        [Required]
        public decimal PVP { get; set; }
        public decimal TOTAL { get; set; } 
        [Range(0, 9999.99, ErrorMessage = "No se permiten números negativos")]
        [Required]
        public decimal CANTIDAD { get; set; }
        public DateTime FECHAREGISTRO { get; set; }
        [Required]
        public string MARCA { get; set; }
        public string MODELO { get; set; }
    }
}