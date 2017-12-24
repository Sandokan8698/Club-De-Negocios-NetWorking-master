namespace Data_Layer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditSubpedidoPedido2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.SubPedidoEntry", "Fecha");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SubPedidoEntry", "Fecha", c => c.DateTime(nullable: false, storeType: "date"));
        }
    }
}
