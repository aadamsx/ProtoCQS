namespace Proto.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateConnectionConfig : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ConnectionConfiguration", "MachineName", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.ConnectionConfiguration", "Setting", c => c.String(nullable: false, maxLength: 128));
            DropPrimaryKey("dbo.ConnectionConfiguration");
            AddPrimaryKey("dbo.ConnectionConfiguration", new[] { "MachineName", "Setting", "Value" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.ConnectionConfiguration");
            AddPrimaryKey("dbo.ConnectionConfiguration", "Value");
            AlterColumn("dbo.ConnectionConfiguration", "Setting", c => c.String());
            AlterColumn("dbo.ConnectionConfiguration", "MachineName", c => c.String());
        }
    }
}
