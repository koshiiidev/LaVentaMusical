using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PAV_PF_JorgeIsaacLopezV.Models
{
    [Table("Perfil")]
    public class Perfil
    {
        [Key]
        [Column("id_perfil")]
        public int IdPerfil { get; set; }

        [Required]
        [Column("nombre_perfil")]
        [StringLength(30)]
        public string NombrePerfil { get; set; }

        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}