using Proto.Domain.QueryHandlers;
using Proto.Model.Entities;

namespace Proto.Domain.Queries.Tenants
{
    public class GetTenantByIdQuery : IQuery<Tenant>
    {
        public int TenantId { get; set; }
    }
}



