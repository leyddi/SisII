using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sistema_Comercial.Models
{
    public class Caja
    {
        public DateTime FECHA_APERTURA { get; set; }
        public decimal SALDO_INICIAL { get; set; }
        public string MONEDA { get; set; }
        public decimal TOTAL { get; set; }
    }
}