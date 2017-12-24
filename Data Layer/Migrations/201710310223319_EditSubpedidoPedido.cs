namespace Data_Layer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditSubpedidoPedido : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SubPedidoEntry", "Deuda", c => c.Decimal(nullable: false, storeType: "money"));
            AddColumn("dbo.SubPedidoEntry", "FechaCreacion", c => c.DateTime(nullable: false, storeType: "date"));
            DropColumn("dbo.SubPedido", "PrecioProveedor");
            DropColumn("dbo.SubPedido", "Abono");
            DropColumn("dbo.SubPedido", "Deuda");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SubPedido", "Deuda", c => c.Decimal(nullable: false, storeType: "money"));
            AddColumn("dbo.SubPedido", "Abono", c => c.Decimal(nullable: false, storeType: "money"));
            AddColumn("dbo.SubPedido", "PrecioProveedor", c => c.Decimal(nullable: false, storeType: "money"));
            DropColumn("dbo.SubPedidoEntry", "FechaCreacion");
            DropColumn("dbo.SubPedidoEntry", "Deuda");
        }
    }
}
