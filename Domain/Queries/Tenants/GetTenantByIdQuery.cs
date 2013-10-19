using Domain.QueryHandlers;

namespace Domain.Queries.Tenants
{
    public class GetTenantByIdQuery : IQuery<DataModel.Tenant>
    {
        public int TenantId { get; set; }
    }
}



