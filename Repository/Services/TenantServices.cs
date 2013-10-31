using System.Collections.Generic;
using System.Linq;
using DataModel;

namespace Repository.Services
{
    public static class TenantServices
    {
        public static string ContactTypeName(this Tenant tenant)
        {
            return tenant.Type.Name;
        }

        public static IEnumerable<Tenant> GetTenants(this ContactType contactType)
        {
            return contactType.Tenants.AsEnumerable();
        }
    }
}
