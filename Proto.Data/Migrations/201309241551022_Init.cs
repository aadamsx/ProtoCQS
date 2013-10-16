namespace Proto.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ContactType",
                c => new
                    {
                        ContactTypeId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.ContactTypeId);
            
            CreateTable(
                "dbo.Tenant",
                c => new
                    {
                        TenantId = c.Int(nullable: false, identity: true),
                        AccountNumber = c.String(maxLength: 10),
                        Name = c.String(nullable: false, maxLength: 50),
                        Active = c.Int(),
                        PrimaryContactFirstName = c.String(nullable: false, maxLength: 50),
                        PrimaryContactLastName = c.String(nullable: false, maxLength: 50),
                        PrimaryContactPhone = c.String(nullable: false, maxLength: 25),
                        Description = c.String(),
                        Email = c.String(maxLength: 50),
                        OfficePhone = c.String(nullable: false, maxLength: 25),
                        Street = c.String(maxLength: 50),
                        City = c.String(maxLength: 50),
                        State = c.String(maxLength: 2),
                        Zip = c.String(maxLength: 10),
                        RowGuid = c.Guid(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                        ContactTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TenantId)
                .ForeignKey("dbo.ContactType", t => t.ContactTypeId, cascadeDelete: true)
                .Index(t => t.ContactTypeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tenant", "ContactTypeId", "dbo.ContactType");
            DropIndex("dbo.Tenant", new[] { "ContactTypeId" });
            DropTable("dbo.Tenant");
            DropTable("dbo.ContactType");
        }
    }
}
