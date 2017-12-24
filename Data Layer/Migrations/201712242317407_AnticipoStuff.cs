namespace Data_Layer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AnticipoStuff : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Anticipo", "ClienteId", "dbo.Cliente");
            DropIndex("dbo.Anticipo", new[] { "ClienteId" });
           
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Anticipo",
                c => new
                    {
                        AnticipoId = c.Int(nullable: false, identity: true),
                        ClienteId = c.Int(nullable: false),
                        Valor = c.Decimal(nullable: false, storeType: "money"),
                        Fecha = c.DateTime(nullable: false, storeType: "date"),
                    })
                .PrimaryKey(t => t.AnticipoId);
            
            CreateIndex("dbo.Anticipo", "ClienteId");
            AddForeignKey("dbo.Anticipo", "ClienteId", "dbo.Cliente", "ClienteId", cascadeDelete: true);
        }
    }
}
