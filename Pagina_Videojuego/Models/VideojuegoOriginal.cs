using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pagina_Videojuego.Models
{
    public class VideojuegoOriginal
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese el Código")]
        [DisplayName("Codigo del Videojuego")]
        public string idVid { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese el Nombre")]
        [DisplayName("Nombre del Videojuego")]
        public string nomVide { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese las Plataformas")]
        [DisplayName("Plataformas del Videojuego")]
        public string platafor { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese la Desarrolladora")]
        [DisplayName("Nombre del Desarrollador")]
        public string desarro { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese la Fecha")]
        [DisplayName("Fecha de Lanzamiento")]
        public string fechlan { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese el Precio")]
        [DisplayName("Precio del Videojuego")]
        public double precio { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese el Stock")]
        [DisplayName("Stock")]
        public int stock { get; set; }

        public string portada { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese el Genero")]
        [DisplayName("Genero del Videojuego")]
        public string genero { get; set; }
    }

}