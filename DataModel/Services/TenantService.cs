using System.Collections.Generic;
using System.Linq;

namespace DataModel.Services
{
    public static class TenantService
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
