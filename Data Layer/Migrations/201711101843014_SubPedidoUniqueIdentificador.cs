namespace Data_Layer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SubPedidoUniqueIdentificador : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SubPedido", "Identificador", c => c.String(nullable: false, maxLength: 450, unicode: false));
            CreateIndex("dbo.SubPedido", "Identificador");
        }
        
        public override void Down()
        {
            DropIndex("dbo.SubPedido", new[] { "Identificador" });
            AlterColumn("dbo.SubPedido", "Identificador", c => c.String(nullable: false));
        }
    }
}
