using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sistema_Comercial.Models
{
    public class Caja
    {
        public DateTime FECHA_APERTURA { get; set; }
        [Display(Name = "MONTO DE APERTURA")]
        public decimal SALDO_INICIAL { get; set; }
        public string MONEDA { get; set; }
        public string ID_MONEDA { get; set; }
        public decimal TOTAL { get; set; }
    }
}