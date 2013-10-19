using Domain.QueryHandlers;

namespace Domain.Queries.Tenants
{
    public class GetTenantByIdQuery : IQuery<Domain.Model.Tenant>
    {
        public int TenantId { get; set; }
    }
}



