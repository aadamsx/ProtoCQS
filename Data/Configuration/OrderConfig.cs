using System;
using System.Data.Entity.ModelConfiguration;
using DataModel;

namespace Data.Configuration
{
    public class OrderConfig : EntityTypeConfiguration<Order>
    {
        public OrderConfig()
        {
            // Primary Key
            HasKey(t => t.OrderId);

            // Composite Primary Key
            //HasKey(t => new { t.OrderId, t.TenantId });
        }
        //public int OrderId { get; set; } // PK
        //public int TenantId { get; set; } // PK

        //public int CustomerId { get; set; }
        ////public int EmployeeId { get; set; }
        //public int PaymentId { get; set; }
        //public string OrderNumber { get; set; }
        //public DateTime OrderDate { get; set; }
        //public string SubTotal { get; set; }
        //public string TaxAmt { get; set; }
        //public string TotalDue { get; set; }
        //public DateTime ModifiedDate { get; set; }
    }
    public class OrderLineConfig : EntityTypeConfiguration<OrderLine>
    {
        public OrderLineConfig()
        {
            // Primary Key
            HasKey(t => t.OrderLineId);
        }
        //public int OrderLineId { get; set; } // PK
        ////public int TenantId { get; set; } // PK

        //public int CustomerId { get; set; }
        //public int OrderId { get; set; }

        //public int ProductId { get; set; }

        ////public int OrderQty { get; set; }
        //public string UnitPrice { get; set; }
        //public string LineTotal { get; set; }
        //public DateTime ModifiedDate { get; set; }
    }

    public class PaymentConfig : EntityTypeConfiguration<Payment>
    {
        public PaymentConfig()
        {
            // Primary Key
            HasKey(t => t.PaymentId);

        }
        //public int PaymentId { get; set; }
        //public string PaymentType { get; set; }
        //public bool Allowed { get; set; }
    }
}
