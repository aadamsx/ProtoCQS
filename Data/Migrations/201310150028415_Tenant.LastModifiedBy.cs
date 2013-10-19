namespace Proto.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TenantLastModifiedBy : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tenant", "LastModifiedBy", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tenant", "LastModifiedBy");
        }
    }
}
