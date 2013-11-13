using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class Order
    {
        public int OrderId { get; set; } // PK
        public int TenantId { get; set; } // PK


        public int CustomerId { get; set; }
        //public int EmployeeId { get; set; }
        public int PaymentId { get; set; }
        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public string SubTotal { get; set; }
        public string TaxAmt { get; set; }
        public string TotalDue { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
    public class OrderLine
    {
        public int OrderLineId { get; set; } // PK
        //public int TenantId { get; set; } // PK
     
        public int CustomerId { get; set; }
        public int OrderId { get; set; }     

        public int ProductId { get; set; }

        //public int OrderQty { get; set; }
        public string UnitPrice { get; set; }
        public string LineTotal { get; set; }
        public DateTime ModifiedDate { get; set; }
    }

    public class Payment
    {
        public int PaymentId { get; set; }
        public string PaymentType { get; set; }
        public bool Allowed { get; set; }
    }
}
