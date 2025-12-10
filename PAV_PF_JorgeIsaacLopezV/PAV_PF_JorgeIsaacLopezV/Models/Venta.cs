using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PAV_PF_JorgeIsaacLopezV.Models
{
    [Table("Venta")]
    public class Venta
    {
        [Key]
        [Column("id_venta")]
        public int IdVenta { get; set; }

        [Required]
        [Column("numero_factura")]
        [StringLength(20)]
        public string NumeroFactura { get; set; }

        [Required]
        [Column("fecha_compra")]
        public DateTime FechaCompra { get; set; }

        [Required]
        [Column("subtotal")]
        public decimal Subtotal { get; set; }

        [Required]
        [Column("iva")]
        public decimal Iva { get; set; }

        [Required]
        [Column("total")]
        public decimal Total { get; set; }

        [Required]
        [Column("id_usuario")]
        public int IdUsuario { get; set; }


        [ForeignKey("IdUsuario")]
        public virtual Usuario Usuario { get; set; }

        public virtual ICollection<DetalleVenta> DetallesVenta { get; set; }

    }
}