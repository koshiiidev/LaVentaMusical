using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PAV_PF_JorgeIsaacLopezV.Models
{
    public class LaVentaMusicalContext : DbContext
    {
        // El nombre debe coincidir con el connectionString en Web.config
        public LaVentaMusicalContext()
            : base("name=LaVentaMusicalContext")
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Perfil> Perfiles { get; set; }
        public DbSet<TipoTarjeta> TiposTarjeta { get; set; }
        public DbSet<GeneroMusical> GenerosMusicales { get; set; }
        public DbSet<Cancion> Canciones { get; set; }
        public DbSet<Venta> Ventas { get; set; }
        public DbSet<DetalleVenta> DetallesVenta { get; set; }
    }
}
