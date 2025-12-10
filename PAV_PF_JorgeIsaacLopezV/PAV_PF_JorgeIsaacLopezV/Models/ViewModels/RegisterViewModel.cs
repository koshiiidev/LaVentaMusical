using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PAV_PF_JorgeIsaacLopezV.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Número de identificación")]
        public string NumeroIdentificacion { get; set; }

        [Required]
        [Display(Name = "Nombre completo")]
        public string NombreCompleto { get; set; }

        [Required]
        [Display(Name = "Género")]
        public string Genero { get; set; } 

        [Required]
        [EmailAddress]
        [Display(Name = "Correo electrónico")]
        public string Correo { get; set; }

        [Required]
        [Display(Name = "Tipo de tarjeta")]
        public int IdTipoTarjeta { get; set; }

        [Required]
        [Display(Name = "Número de tarjeta")]
        public string NumeroTarjeta { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Contrasena { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar contraseña")]
        [Compare("Contrasena", ErrorMessage = "Las contraseñas no coinciden.")]
        public string ConfirmarContrasena { get; set; }
    }
}