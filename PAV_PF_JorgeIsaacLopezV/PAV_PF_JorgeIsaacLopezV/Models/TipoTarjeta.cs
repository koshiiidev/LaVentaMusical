using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PAV_PF_JorgeIsaacLopezV.Models
{
    [Table("TipoTarjeta")]
    public class TipoTarjeta
    {
        [Key]
        [Column("id_tipo_tarjeta")]
        public int IdTipoTarjeta { get; set; }

        [Required]
        [Column("nombre_tarjeta")]
        [StringLength(30)]
        public string NombreTarjeta { get; set; }


        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}