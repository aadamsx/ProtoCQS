namespace Proto.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RowVersion : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tenant", "RowVersion", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
            DropColumn("dbo.Tenant", "ModifiedDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tenant", "ModifiedDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Tenant", "RowVersion");
        }
    }
}
