using System;

namespace DataModel
{
    public class Product
    {
        public int ProductId { get; set; } // PK
        public string Name { get; set; } // Req
        public string ProductNumber { get; set; } // Req
        public string ProductDescription { get; set; } // Req
        public string ListPrice { get; set; } // Req
        public int ProductSubcategoryId { get; set; }
        public int ProductPackageId { get; set; } 
        public DateTime SellStartDate { get; set; } // Req
        public DateTime SellEndDate { get; set; }
        public DateTime DiscontinuedDate { get; set; }
        public Guid RowGuid { get; set; } // Req
        public DateTime ModifiedDate { get; set; } // Req
    }

    public class ProductPackage
    {
        public int ProductPackageId { get; set; } // PK
        public string Name { get; set; } // Req
        public string PackageDescription { get; set; }
        public Guid RowGuid { get; set; } // Req
        public DateTime ModifiedDate { get; set; } // Req
    }

    public class ProductSubCategory
    {
        public int ProductSubCategoryId { get; set; } // PK
        public int ProductCategoryId { get; set; } // Req
        public string Name { get; set; } // Req
        public Guid RowGuid { get; set; } // Req
        public DateTime ModifiedDate { get; set; } // Req
    }

    public class ProductCategory
    {
        public int ProductCategoryId { get; set; } // PK
        public string Name { get; set; } // Req
        public Guid RowGuid { get; set; } // Req
        public DateTime ModifiedDate { get; set; } // Req
    }

    public class ProductListPriceHistory
    {
        //public int ProductListPriceHistoryId { get; set; } 
        public int ProductId { get; set; } // PK
        public DateTime StartDate { get; set; } // PK
        public DateTime EndDate { get; set; }
        public string ListPrice { get; set; } // Req
        public DateTime ModifiedDate { get; set; } // Req
    }

    public class TransactionHistory
    {
        public int TransactionId { get; set; }
        public int ProductId { get; set; }
        public int ReferenceOrderId { get; set; }
        public int ReferenceOrderLineId { get; set; }
        public DateTime TransactionDate { get; set; }
        public string TransactionType { get; set; }
        //public int Quantity { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
