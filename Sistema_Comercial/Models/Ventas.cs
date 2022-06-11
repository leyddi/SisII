using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sistema_Comercial.Models
{
    public class Ventas
    {
        public int ID { get; set; }
        public DateTime FECHAREGISTRO { get; set; }
        public Usuario USUARIO { get; set; }
        public decimal TOTAL { get; set; }
        public Cliente CLIENTE { get; set; }
        public Serie SERIE { get; set; }
        public List<Productos> PRODUCTOS { get; set; }
        public Pago PAGO { get; set; }
        public string COMPROBANTE { get; set; }
    }
}