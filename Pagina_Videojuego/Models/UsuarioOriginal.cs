using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pagina_Videojuego.Models
{
    public class UsuarioOriginal
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese Código")]
        [DisplayName("Codigo de Usuario")]
        public string idUsuario { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese Nombre de Usuario")]
        [DisplayName("Nombre de Usuario")]
        [StringLength(30,ErrorMessage ="maximo 30 letras")]
        public string nombres { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese nombre del usuario")]
        [StringLength(30, ErrorMessage = "maximo 30 letras")]
        [DisplayName("Nombre del usuario")]
        public string nombreUsu { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese ")]
        [StringLength(15, ErrorMessage = "maximo 15 letras")]
        [DisplayName("Contraseña ")]
        public string password { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "INGRESE CORREO")]
        [DisplayName("Correo")]
        [EmailAddress(ErrorMessage ="formato de correo no valido")]
        public string correo { get; set; }
        
        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese FECHA")]
        [DisplayName("Fecha Nacimiento")]
        public DateTime fechaNaci { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese SEXO")]
        [StringLength(10, ErrorMessage = "maximo 10 letras")]
        [DisplayName("Sexo")]
        public String sexo { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingrese Pais")]
        [DisplayName("Pais")]
        [StringLength(4, ErrorMessage = "maximo 4 letras")]
        public string idpais { get; set; }

        
    }
}