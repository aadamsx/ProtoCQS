using System.Linq;
using Data;
using DataModel;
using Domain.Queries.Tenants;

namespace Domain.QueryHandlers
{
    public class TenantQueryHandlers :
        IQueryHandler<GetTenantByIdQuery, Tenant>,
        //IQueryHandler<GetTenantsQuery, IEnumerable<Tenant>>,
        IQueryHandler<GetCurrentTenantsQuery, IQueryable<Tenant>>
    {
        private ClientManagementContext db;

        public TenantQueryHandlers
            (ClientManagementContext context)
        {
            db = context;
        }

        public IQueryable<Tenant> Handle(GetCurrentTenantsQuery query)
        {
            //Note: temp removal
            //return db.Tenants
            //    .AsNoTracking()
            //    //.OrderByDescending(t => t.Name)
            //    .OrderBy(t => t.Name)
            //    .Skip((query.PageIndex - 1) * query.PageSize)
            //    .Take(query.PageSize);

            return null;
        }

        public Tenant Handle(GetTenantByIdQuery query)
        {
            // If the return type was/is IQueryable<AllTenants>
            //return
            //    from tenant in context.AllTenants
            //    where tenant.TenantId == query.TenantId
            //    select tenant;

            //Note: temp removal

            //return db.Tenants
            //    .Find(query.TenantId);

            return null;
        }
    }
}

