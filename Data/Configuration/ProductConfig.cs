using System.Data.Entity.ModelConfiguration;
using DataModel;

namespace Data.Configuration
{
    public class ProductConfig : EntityTypeConfiguration<Product>
    {
        public ProductConfig()
        {
            // Primary Key
            HasKey(t => t.ProductId);
        }

        //public int ProductId { get; set; } // PK
        //public string Name { get; set; } // Req
        //public string ProductNumber { get; set; } // Req
        //public string ProductDescription { get; set; } // Req
        //public string ListPrice { get; set; } // Req
        //public int ProductSubcategoryId { get; set; }
        //public int ProductPackageId { get; set; }
        //public DateTime SellStartDate { get; set; } // Req
        //public DateTime SellEndDate { get; set; }
        //public DateTime DiscontinuedDate { get; set; }
        //public Guid RowGuid { get; set; } // Req
        //public DateTime ModifiedDate { get; set; } // Req
    }

    public class ProductPackageConfig : EntityTypeConfiguration<ProductPackage>
    {
        public ProductPackageConfig()
        {
            // Primary Key
            HasKey(t => t.ProductPackageId);
        }
        //public int ProductPackageId { get; set; } // PK
        //public string Name { get; set; } // Req
        //public string PackageDescription { get; set; }
        //public Guid RowGuid { get; set; } // Req
        //public DateTime ModifiedDate { get; set; } // Req
    }

    public class ProductSubCategoryConfig : EntityTypeConfiguration<ProductSubCategory>
    {
        public ProductSubCategoryConfig()
        {
            // Primary Key
            HasKey(t => t.ProductSubCategoryId);
        }
        //public int ProductSubCategoryId { get; set; } // PK
        //public int ProductCategoryId { get; set; } // Req
        //public string Name { get; set; } // Req
        //public Guid RowGuid { get; set; } // Req
        //public DateTime ModifiedDate { get; set; } // Req
    }

    public class ProductCategoryConfig : EntityTypeConfiguration<ProductCategory>
    {
        public ProductCategoryConfig()
        {
            // Primary Key
            HasKey(t => t.ProductCategoryId);
        }
        //public int ProductCategoryId { get; set; } // PK
        //public string Name { get; set; } // Req
        //public Guid RowGuid { get; set; } // Req
        //public DateTime ModifiedDate { get; set; } // Req
    }

    public class ProductListPriceHistoryConfig : EntityTypeConfiguration<ProductListPriceHistory>
    {
        public ProductListPriceHistoryConfig()
        {
            // Composite Primary Key
            HasKey(t => new { t.ProductId, t.StartDate });
        }
        //public int ProductId { get; set; } // PK
        //public DateTime StartDate { get; set; } // PK
        //public DateTime EndDate { get; set; }
        //public string ListPrice { get; set; } // Req
        //public DateTime ModifiedDate { get; set; } // Req
    }

    public class TransactionHistoryConfig : EntityTypeConfiguration<TransactionHistory>
    {
        public TransactionHistoryConfig()
        {
            // Primary Key
            HasKey(t => t.TransactionId);
        }
        //public int TransactionId { get; set; }
        //public int ProductId { get; set; }
        //public int ReferenceOrderId { get; set; }
        //public int ReferenceOrderLineId { get; set; }
        //public DateTime TransactionDate { get; set; }
        //public string TransactionType { get; set; }
        ////public int Quantity { get; set; }
        //public DateTime ModifiedDate { get; set; }
    }
}
