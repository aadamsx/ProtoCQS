using System;
using System.Collections.Generic;

namespace DataModel
{
    // The paying Person or point of contact for the Company
    public class Customer
    {
        public int CustomerId { get; set; }
        public int TenantId { get; set; }
        public string Forename { get; set; }
        public string Surename { get; set; }

    }

    // FormsPoint's Clients (These are Companies)
    public class Tenant
    {
        public Tenant()
        {
            //BillingAddress = new Address();
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


        // public Address BillingAddress { get; set; }

        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }

        public Guid RowGuid { get; set; }

        public string LastModifiedBy { get; set; }
        public Byte[] RowVersion { get; set; }

        // ...

        // Relations and Navigation
        public int ContactTypeId { get; set; }
        public virtual ContactType Type { get; set; }
    }

    public class ContactType
    {
        // Primary key
        public int ContactTypeId { get; set; }

        // Property
        public string Name { get; set; }

        // Entity mapping
        public virtual ICollection<Tenant> Tenants { get; set; }
    }

    public class Account
    {
        public string Id { get; set; }
        public string AccountNumber { get; set; }
    }
}
