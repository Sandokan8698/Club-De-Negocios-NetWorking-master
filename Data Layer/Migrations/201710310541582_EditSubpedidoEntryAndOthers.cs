namespace Data_Layer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditSubpedidoEntryAndOthers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SubPedido", "PrecioProveedor", c => c.Decimal(nullable: false, storeType: "money"));
            DropColumn("dbo.SubPedidoEntry", "PrecioProveedor");
            DropColumn("dbo.SubPedidoEntry", "Deuda");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SubPedidoEntry", "Deuda", c => c.Decimal(nullable: false, storeType: "money"));
            AddColumn("dbo.SubPedidoEntry", "PrecioProveedor", c => c.Decimal(nullable: false, storeType: "money"));
            DropColumn("dbo.SubPedido", "PrecioProveedor");
        }
    }
}
