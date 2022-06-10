using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sistema_Comercial.Models
{
    public class Pago
    {
        public int ID { get; set; }
        public decimal MONTO { get; set; }
        public List<DetallePago> DETALLEPAGOS { get; set; }
    }
}