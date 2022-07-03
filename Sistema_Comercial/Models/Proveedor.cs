using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sistema_Comercial.Models
{
    public class Proveedor
    {
        public int ID { get; set; }
        public string DESCRIPCION { get; set; }
        public string DIRECCION { get; set; }
        [Display(Name = "NÚMERO")]
        public string NUMERODOCUMENTO { get; set; }
        public int ID_TIPODOCUMENTO { get; set; }
        [Display(Name = "TIPO DOCUMENTO")]
        public string TIPODOCUMENTO { get; set; }
    }
}