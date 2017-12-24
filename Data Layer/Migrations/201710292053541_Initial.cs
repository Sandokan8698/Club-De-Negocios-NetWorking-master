namespace Data_Layer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cliente",
                c => new
                    {
                        ClienteId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 50),
                        Apellidos = c.String(maxLength: 50),
                        FechaNacimiento = c.DateTime(nullable: false, storeType: "date"),
                        NumeroDocumento = c.String(nullable: false, maxLength: 11),
                        Direccion = c.String(maxLength: 100),
                        Telefono = c.String(maxLength: 10),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.ClienteId)
                .Index(t => t.NumeroDocumento, unique: true);
            
            CreateTable(
                "dbo.Venta",
                c => new
                    {
                        VentaId = c.Int(nullable: false, identity: true),
                        ClienteId = c.Int(nullable: false),
                        TrabajadorId = c.Int(nullable: false),
                        Fecha = c.DateTime(nullable: false, storeType: "date"),
                        Serie = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.VentaId)
                .ForeignKey("dbo.Cliente", t => t.ClienteId, cascadeDelete: true)
                .ForeignKey("dbo.Trabajador", t => t.TrabajadorId)
                .Index(t => t.ClienteId)
                .Index(t => t.TrabajadorId);
            
            CreateTable(
                "dbo.Pedido",
                c => new
                    {
                        PedidoId = c.Int(nullable: false, identity: true),
                        ItemNumero = c.Int(nullable: false),
                        ProveedorId = c.Int(nullable: false),
                        VentaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PedidoId)
                .ForeignKey("dbo.Proveedor", t => t.ProveedorId, cascadeDelete: true)
                .ForeignKey("dbo.Venta", t => t.VentaId, cascadeDelete: true)
                .Index(t => t.ProveedorId)
                .Index(t => t.VentaId);
            
            CreateTable(
                "dbo.Proveedor",
                c => new
                    {
                        ProveedorId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 100),
                        Direccion = c.String(maxLength: 100),
                        Telefono = c.String(maxLength: 10),
                        NumeroCuenta = c.Int(),
                        Email = c.String(),
                        Url = c.String(),
                    })
                .PrimaryKey(t => t.ProveedorId);
            
            CreateTable(
                "dbo.SubPedido",
                c => new
                    {
                        SubPedidoId = c.Int(nullable: false, identity: true),
                        PedidoId = c.Int(nullable: false),
                        PrecioProveedor = c.Decimal(nullable: false, storeType: "money"),
                        Identificador = c.String(nullable: false),
                        Abono = c.Decimal(nullable: false, storeType: "money"),
                        Deuda = c.Decimal(nullable: false, storeType: "money"),
                        FechaCreacion = c.DateTime(nullable: false, storeType: "date"),
                        FechaActualizacion = c.DateTime(nullable: false, storeType: "date"),
                    })
                .PrimaryKey(t => t.SubPedidoId)
                .ForeignKey("dbo.Pedido", t => t.PedidoId, cascadeDelete: true)
                .Index(t => t.PedidoId);
            
            CreateTable(
                "dbo.SubPedidoEntry",
                c => new
                    {
                        SubPedidoEntryId = c.Int(nullable: false, identity: true),
                        SubPedidoId = c.Int(nullable: false),
                        TrabajadorId = c.Int(),
                        Fecha = c.DateTime(nullable: false, storeType: "date"),
                        PrecioProveedor = c.Decimal(nullable: false, storeType: "money"),
                        Abono = c.Decimal(nullable: false, storeType: "money"),
                    })
                .PrimaryKey(t => t.SubPedidoEntryId)
                .ForeignKey("dbo.SubPedido", t => t.SubPedidoId, cascadeDelete: true)
                .ForeignKey("dbo.Trabajador", t => t.TrabajadorId)
                .Index(t => t.SubPedidoId)
                .Index(t => t.TrabajadorId);
            
            CreateTable(
                "dbo.Trabajador",
                c => new
                    {
                        TrabajadorId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 20),
                        Apellidos = c.String(nullable: false, maxLength: 40),
                        Sexo = c.String(nullable: false, maxLength: 1),
                        FechaNacimiento = c.DateTime(nullable: false, storeType: "date"),
                        NumeroDocumento = c.String(nullable: false, maxLength: 11),
                        Direccion = c.String(maxLength: 100),
                        Telefono = c.String(maxLength: 10),
                        Email = c.String(),
                        Acceso = c.String(nullable: false, maxLength: 20),
                        Usuario = c.String(nullable: false, maxLength: 20),
                        Password = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.TrabajadorId);
            
            CreateTable(
                "dbo.Campanlla",
                c => new
                    {
                        CampanllaId = c.Int(nullable: false, identity: true),
                        ProveedorId = c.Int(nullable: false),
                        FechaInicio = c.DateTime(nullable: false, storeType: "date"),
                        FechaCierre = c.DateTime(nullable: false, storeType: "date"),
                        IsActive = c.Boolean(nullable: false),
                        Numero = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CampanllaId)
                .ForeignKey("dbo.Proveedor", t => t.ProveedorId)
                .Index(t => t.ProveedorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Campanlla", "ProveedorId", "dbo.Proveedor");
            DropForeignKey("dbo.Venta", "TrabajadorId", "dbo.Trabajador");
            DropForeignKey("dbo.Pedido", "VentaId", "dbo.Venta");
            DropForeignKey("dbo.SubPedido", "PedidoId", "dbo.Pedido");
            DropForeignKey("dbo.SubPedidoEntry", "TrabajadorId", "dbo.Trabajador");
            DropForeignKey("dbo.SubPedidoEntry", "SubPedidoId", "dbo.SubPedido");
            DropForeignKey("dbo.Pedido", "ProveedorId", "dbo.Proveedor");
            DropForeignKey("dbo.Venta", "ClienteId", "dbo.Cliente");
            DropIndex("dbo.Campanlla", new[] { "ProveedorId" });
            DropIndex("dbo.SubPedidoEntry", new[] { "TrabajadorId" });
            DropIndex("dbo.SubPedidoEntry", new[] { "SubPedidoId" });
            DropIndex("dbo.SubPedido", new[] { "PedidoId" });
            DropIndex("dbo.Pedido", new[] { "VentaId" });
            DropIndex("dbo.Pedido", new[] { "ProveedorId" });
            DropIndex("dbo.Venta", new[] { "TrabajadorId" });
            DropIndex("dbo.Venta", new[] { "ClienteId" });
            DropIndex("dbo.Cliente", new[] { "NumeroDocumento" });
            DropTable("dbo.Campanlla");
            DropTable("dbo.Trabajador");
            DropTable("dbo.SubPedidoEntry");
            DropTable("dbo.SubPedido");
            DropTable("dbo.Proveedor");
            DropTable("dbo.Pedido");
            DropTable("dbo.Venta");
            DropTable("dbo.Cliente");
        }
    }
}
