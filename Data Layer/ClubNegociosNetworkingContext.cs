using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Domain_Layer.Entities;


namespace Data_Layer
{
    public class ClubNegociosNetworkingContext: DbContext
    {
        public ClubNegociosNetworkingContext():base("name=ClubNegociosNetworkingData")
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }


        public DbSet<Proveedor> ProveedorDbSet { get; set; }
        public DbSet<Trabajador> TrabajadorDbSet { get; set; }
        public DbSet<Cliente> ClienteDbSet { get; set; }
        public DbSet<Venta> VentaDbSet { get; set; }
        public DbSet<Pedido> PedidoDbSet { get; set; }
        public DbSet<SubPedido> SubPedidoDbSet { get; set; }
        public DbSet<SubPedidoEntry> SubPedidoEntriesDbSet { get; set; }
       


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<Venta>().HasRequired(v => v.Trabajador).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<Campanlla>().HasRequired(v => v.Proveedor).WithMany().WillCascadeOnDelete(false);
           // modelBuilder.Entity<SubPedido>().Property(sbe => sbe.Identificador).IsRequired().HasMaxLength(450).HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("Index") { IsUnique = true } }));
            //    modelBuilder.Entity<DetalleIngreso>().Property(t => t.FechaProduccion).HasColumnType("date");
            //    modelBuilder.Entity<DetalleIngreso>().Property(t => t.FechaVencimiento).HasColumnType("date");
        }
    }
}
