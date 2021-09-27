using System;
using System.ComponentModel;

namespace Pagina_Videojuego.Models
{
    public class Videojuego
    {
        [DisplayName("CODIGO")]
        public string idVid { get; set; }
        [DisplayName("NOMBRE")]
        public string nomVide { get; set; }
        [DisplayName("PLATAFORMA")]
        public string platafor { get; set; }
        [DisplayName("DESARROLLADOR")]
        public string desarro { get; set; }
        [DisplayName("LANZAMIENTO")]
        public string fechlan { get; set; }
        [DisplayName("PRECIO")]
        public double precio { get; set; }
        [DisplayName("STOCK")]
        public int stock { get; set; }
        [DisplayName("PORTADA")]
        public string portada { get; set; }
        [DisplayName("GENERO")]
        public string genero { get; set; }

    }
}