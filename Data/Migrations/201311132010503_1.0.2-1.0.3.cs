namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _102103 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customer",
                c => new
                    {
                        CustomerId = c.Int(nullable: false, identity: true),
                        TenantId = c.Int(nullable: false),
                        Forename = c.String(),
                        Surename = c.String(),
                    })
                .PrimaryKey(t => t.CustomerId);
            
            CreateTable(
                "dbo.Order",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        TenantId = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        PaymentId = c.Int(nullable: false),
                        OrderNumber = c.String(),
                        OrderDate = c.DateTime(nullable: false),
                        SubTotal = c.String(),
                        TaxAmt = c.String(),
                        TotalDue = c.String(),
                        ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.OrderId);
            
            CreateTable(
                "dbo.OrderLine",
                c => new
                    {
                        OrderLineId = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        OrderId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        UnitPrice = c.String(),
                        LineTotal = c.String(),
                        ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.OrderLineId);
            
            CreateTable(
                "dbo.Payment",
                c => new
                    {
                        PaymentId = c.Int(nullable: false, identity: true),
                        PaymentType = c.String(),
                        Allowed = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PaymentId);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ProductNumber = c.String(),
                        ProductDescription = c.String(),
                        ListPrice = c.String(),
                        ProductSubcategoryId = c.Int(nullable: false),
                        ProductPackageId = c.Int(nullable: false),
                        SellStartDate = c.DateTime(nullable: false),
                        SellEndDate = c.DateTime(nullable: false),
                        DiscontinuedDate = c.DateTime(nullable: false),
                        RowGuid = c.Guid(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ProductId);
            
            CreateTable(
                "dbo.ProductPackage",
                c => new
                    {
                        ProductPackageId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        PackageDescription = c.String(),
                        RowGuid = c.Guid(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ProductPackageId);
            
            CreateTable(
                "dbo.ProductSubCategory",
                c => new
                    {
                        ProductSubCategoryId = c.Int(nullable: false, identity: true),
                        ProductCategoryId = c.Int(nullable: false),
                        Name = c.String(),
                        RowGuid = c.Guid(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ProductSubCategoryId);
            
            CreateTable(
                "dbo.ProductCategory",
                c => new
                    {
                        ProductCategoryId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        RowGuid = c.Guid(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ProductCategoryId);
            
            CreateTable(
                "dbo.ProductListPriceHistory",
                c => new
                    {
                        ProductId = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        ListPrice = c.String(),
                        ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProductId, t.StartDate });
            
            CreateTable(
                "dbo.TransactionHistory",
                c => new
                    {
                        TransactionId = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        ReferenceOrderId = c.Int(nullable: false),
                        ReferenceOrderLineId = c.Int(nullable: false),
                        TransactionDate = c.DateTime(nullable: false),
                        TransactionType = c.String(),
                        ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.TransactionId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TransactionHistory");
            DropTable("dbo.ProductListPriceHistory");
            DropTable("dbo.ProductCategory");
            DropTable("dbo.ProductSubCategory");
            DropTable("dbo.ProductPackage");
            DropTable("dbo.Product");
            DropTable("dbo.Payment");
            DropTable("dbo.OrderLine");
            DropTable("dbo.Order");
            DropTable("dbo.Customer");
        }
    }
}
