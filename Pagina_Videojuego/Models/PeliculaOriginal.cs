using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pagina_Videojuego.Models
{
    public class PeliculaOriginal
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese Código")]
        [DisplayName("Codigo de Pelicula")]
        public string idPelicula { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese Nombre de Pelicula")]

        [DisplayName("Nombre de Pelicula")]
        [StringLength(30, ErrorMessage ="maximo 30 letras")]
        public string nombre { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese nombre del Director")]
        [DisplayName("Nombre del Director")]
        public string direct { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese la duracion")]
        [DisplayName("Duracion de Pelicula")]
        public string  duracion { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese Fecha de Estreno")]
        [DisplayName("Estreno de Pelicula")]
        public string fechaEstreno { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese Precio")]
        [DisplayName("Precio de Pelicula")]
        public double precio { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese Stock")]
        [DisplayName("Stock de Pelicula")]
        [Range(1, int.MaxValue, ErrorMessage ="Error de Rango")]
        [RegularExpression("(^[0-9]{3}+$)", ErrorMessage = "Solo se permiten números")]
        public int stock { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese Genero")]
        [DisplayName("Genero de Pelicula")]
        public string idgenero { get; set; }

        public string foto { get; set; }
    }
}