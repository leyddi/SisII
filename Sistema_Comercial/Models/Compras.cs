using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sistema_Comercial.Models
{
    public class Compras
    {
        public int ID { get; set; }
        public DateTime FECHAREGISTRO { get; set; }
        public Usuario USUARIO { get; set; }
        public decimal TOTAL { get; set; }
        public Proveedor PROVEEDOR { get; set; }
        [Display(Name = "NÚMERO COMPROBANTE")]
        public string SERIE { get; set; }
        public List<Productos> PRODUCTOS { get; set; }

    }
}