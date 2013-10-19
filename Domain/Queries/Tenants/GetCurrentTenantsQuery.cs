using System.Linq;
using Domain.QueryHandlers;

namespace Domain.Queries.Tenants
{
    public class GetCurrentTenantsQuery : IQuery<IQueryable<Domain.Model.Tenant>>    
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}

