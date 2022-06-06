using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sistema_Comercial.Models
{
    public class Cliente
    {
        public int ID { get; set; }
        public string NOMBRES { get; set; }
        public string NUMERODOCUMENTO { get; set; }
        public int ID_TIPODOCUMENTO { get; set; }
        public string TIPODOCUMENTO { get; set; }
    }
}