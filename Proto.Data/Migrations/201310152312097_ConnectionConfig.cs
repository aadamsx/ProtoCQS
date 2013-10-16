namespace Proto.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConnectionConfig : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ConnectionConfiguration",
                c => new
                    {
                        Value = c.String(nullable: false, maxLength: 128),
                        MachineName = c.String(),
                        Setting = c.String(),
                        Type = c.String(),
                        Provider = c.String(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Value);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ConnectionConfiguration");
        }
    }
}
