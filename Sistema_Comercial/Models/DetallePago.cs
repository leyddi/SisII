using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sistema_Comercial.Models
{
    public class DetallePago
    {
      public int ID { get; set; }
      public int ID_DETALLE_CAJA { get; set; }
      public DetalleCaja DETALLECAJA { get; set; }
    }
}