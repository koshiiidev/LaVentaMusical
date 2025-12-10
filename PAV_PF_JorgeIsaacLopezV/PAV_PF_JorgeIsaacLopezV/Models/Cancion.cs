using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PAV_PF_JorgeIsaacLopezV.Models
{
    [Table("Cancion")]
    public class Cancion
    {
        [Key]
        [Column("id_cancion")]
        public int IdCancion { get; set; }

        [Required]
        [Column("codigo_cancion")]
        [StringLength(10)]
        public string CodigoCancion { get; set; }

        [Required]
        [Column("id_genero")]
        public int IdGenero { get; set; }

        [Required]
        [Column("nombre_cancion")]
        [StringLength(100)]
        public string NombreCancion { get; set; }

        [Required]
        [Column("precio")]
        public decimal Precio { get; set; }


        [ForeignKey("IdGenero")]
        public virtual GeneroMusical GeneroMusical { get; set; }

        public virtual ICollection<DetalleVenta> DetallesVenta { get; set; }


    }
}