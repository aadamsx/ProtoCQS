using System;

namespace Proto.Model.Entities
{
    public class Tenant
    {
        public Tenant()
        {
            BillingAddress = new Address();
        }
        public int TenantId { get; set; }
        public string AccountNumber { get; set; }
        public string Name { get; set; }
        
        //public int? Type { get; set; }

        //public TenantType Type { get; set; }

        public int? Active { get; set; }

        public string PrimaryContactFirstName { get; set; }
        public string PrimaryContactLastName { get; set; }
        public string PrimaryContactPhone { get; set; }

        public string Description { get; set; }
        public string Email { get; set; }
        public string OfficePhone { get; set; }


        public Address BillingAddress { get; set; }

        public Guid RowGuid { get; set; }

        public string LastModifiedBy { get; set; }
        public Byte[] RowVersion { get; set; }

        // ...

        // Relations and Navigation
        public int ContactTypeId { get; set; }
        public virtual ContactType Type { get; set; }
    }
}
