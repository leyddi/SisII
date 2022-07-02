using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sistema_Comercial.Models
{
    public class DetalleCaja
    {
        public decimal MONTO { get; set; }
        public int ID_MONEDA { get; set; }
        public string MONEDA { get; set; }
        public decimal MONTO_BS { get; set; }
        [Display(Name = "TIPO DE CAMBIO")]
        public decimal TIPO_CAMBIO { get; set; }
        public int ID_CAJA { get; set; }
        public string TIPO { get; set; }
        public Boolean APERTURA { get; set; }

    }
}