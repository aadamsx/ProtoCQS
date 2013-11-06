using System.Collections.Generic;

namespace DataModel
{
    public class ContactType
    {
        // Primary key
        public int ContactTypeId { get; set; }

        // Property
        public string Name { get; set; }

        // Entity mapping
        public virtual ICollection<Tenant> Tenants { get; set; }
    }
}
