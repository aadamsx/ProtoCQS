using System.Collections.Generic;
using System.Linq;
using Data;
using DataModel;

namespace Repository.Extension
{
    public class TenantReadRepository
        : ReadRepository<Tenant>
        , ITenantReadRepository
    {
        public TenantReadRepository(
            ClientManagementContext context) 
            : base(context) { }


        public IEnumerable<ContactType> GetContactTypes()
        {
            return Context
                .Set<ContactType>()
                .AsNoTracking()
                .AsEnumerable();
        }

        public static IEnumerable<Tenant> GetByContactType(
            ContactType contactType)
        {
            return contactType.Tenants.AsEnumerable();
        }
    }
}