using System.Linq;
using DataModel;
using Domain.QueryHandlers;

namespace Domain.Queries.Tenants
{
    public class GetCurrentTenantsQuery : IQuery<IQueryable<Tenant>>    
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}

