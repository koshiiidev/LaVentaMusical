using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PAV_PF_JorgeIsaacLopezV.Models
{
    [Table("Usuario")]
    public class Usuario
    {
        [Key]
        [Column("id_usuario")]
        public int IdUsuario { get; set; }

        [Required]
        [Column("numero_identificacion")]
        [StringLength(20)]
        public string NumeroIdentificacion { get; set; }

        [Required]
        [Column("nombre_completo")]
        [StringLength(100)]
        public string NombreCompleto { get; set; }

        [Required]
        [Column("genero")]
        [StringLength(10)]
        public string Genero { get; set; }

        [Required]
        [Column("correo")]
        [StringLength(100)]
        [EmailAddress]
        public string Correo { get; set; }

        [Required]
        [Column("id_tipo_tarjeta")]
        public int IdTipoTarjeta { get; set; }

        [Required]
        [Column("numero_tarjeta")]
        [StringLength(19)]
        public string NumeroTarjeta { get; set; }

        [Required]
        [Column("contrasena")]
        [StringLength(255)]
        public string Contrasena { get; set; }

        [Required]
        [Column("id_perfil")]
        public int IdPerfil { get; set; }

        // Navegación
        [ForeignKey("IdTipoTarjeta")]
        public virtual TipoTarjeta TipoTarjeta { get; set; }

        [ForeignKey("IdPerfil")]
        public virtual Perfil Perfil { get; set; }

        public virtual ICollection<Venta> Ventas { get; set; }

    }
}