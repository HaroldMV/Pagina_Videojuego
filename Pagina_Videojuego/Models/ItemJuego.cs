using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Pagina_Videojuego.Models
{
    public class ItemJuego
    {
        [DisplayName("CODIGO")]
        public string idVid { get; set; }
        [DisplayName("NOMBRE")]
        public string nomVide { get; set; }
        [DisplayName("CANTIDAD")]
        public int cantidad { get; set; }
        [DisplayName("PRECIO S/.")]
        public double precio { get; set; }
        [DisplayName("SUBTOTAL")]
        public double subtotal
        {
            get { return cantidad * precio; }
        }
        [DisplayName("PORTADA")]
        public string portada
        {
            get; set;

        }
    }
}