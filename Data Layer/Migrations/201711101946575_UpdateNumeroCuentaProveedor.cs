namespace Data_Layer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateNumeroCuentaProveedor : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Proveedor", "NumeroCuenta", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Proveedor", "NumeroCuenta", c => c.Int());
        }
    }
}
