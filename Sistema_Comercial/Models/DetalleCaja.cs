﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sistema_Comercial.Models
{
    public class DetalleCaja
    {
        public decimal MONTO { get; set; }
        public int ID_MONEDA { get; set; }
        public decimal MONTO_BS { get; set; }
        public decimal TIPO_CAMBIO { get; set; }
        public int ID_CAJA { get; set; }
    }
}