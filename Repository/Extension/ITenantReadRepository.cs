using System.Collections.Generic;
using DataModel;

namespace Repository.Extension
{
    public interface ITenantReadRepository : IReadRepository<Tenant>
    {
        IEnumerable<ContactType> GetContactTypes();
    }
}