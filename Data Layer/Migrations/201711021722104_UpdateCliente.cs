namespace Data_Layer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCliente : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Trabajador", "Sexo");
            DropColumn("dbo.Trabajador", "FechaNacimiento");
            DropColumn("dbo.Trabajador", "NumeroDocumento");
            DropColumn("dbo.Trabajador", "Direccion");
            DropColumn("dbo.Trabajador", "Telefono");
            DropColumn("dbo.Trabajador", "Email");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Trabajador", "Email", c => c.String());
            AddColumn("dbo.Trabajador", "Telefono", c => c.String(maxLength: 10));
            AddColumn("dbo.Trabajador", "Direccion", c => c.String(maxLength: 100));
            AddColumn("dbo.Trabajador", "NumeroDocumento", c => c.String(nullable: false, maxLength: 11));
            AddColumn("dbo.Trabajador", "FechaNacimiento", c => c.DateTime(nullable: false, storeType: "date"));
            AddColumn("dbo.Trabajador", "Sexo", c => c.String(nullable: false, maxLength: 1));
        }
    }
}
