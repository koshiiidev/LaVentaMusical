using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PAV_PF_JorgeIsaacLopezV.Models
{
    [Table("GeneroMusical")]
    public class GeneroMusical
    {
        [Key]
        [Column("id_genero")]
        public int IdGenero { get; set; }

        [Required]
        [Column("codigo_genero")]
        [StringLength(10)]
        public string CodigoGenero { get; set; }

        [Required]
        [Column("descripcion")]
        [StringLength(100)]
        public string Descripcion { get; set; }

        
        public virtual ICollection<Cancion> Canciones { get; set; }
    }
}