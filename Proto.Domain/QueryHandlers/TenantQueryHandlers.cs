using System.Linq;
using Proto.Data;
using Proto.Domain.Queries.Tenants;
using Proto.Model.Entities;

namespace Proto.Domain.QueryHandlers
{
    public class TenantQueryHandlers :
        IQueryHandler<GetTenantByIdQuery, Tenant>,
        //IQueryHandler<GetTenantsQuery, IEnumerable<Tenant>>,
        IQueryHandler<GetCurrentTenantsQuery, IQueryable<Tenant>>
    {
        //private ClientManagementContextEntities context;

        private ClientManagementContext db;

        //public TenantQueryHandlers
        //    (ClientManagementContextEntities context)
        //{
        //    this.context = context;
        //}

        public TenantQueryHandlers
            (ClientManagementContext context)
        {
            db = context;
        }

        //public Tenant Handle(GetTenantByIdQuery query)
        //{
        //return context.Tenants.Find(query.TenatId);
        //}

        //public IEnumerable<Tenant> Handle(GetTenantsQuery query)
        //{
        //    return context.Tenants
        //        .Skip((query.PageIndex - 1) * query.PageSize)
        //        .Take(query.PageSize)
        //        .ToList(); 
        //}

        //IEnumerable<AllTenants>
        //    IQueryHandler<GetCurrentTenantsQuery, IEnumerable<AllTenants>>
        //    .Handle(GetCurrentTenantsQuery query)
        //{
        //    return context.AllTenants
        //        .AsNoTracking()
        //        //.OrderByDescending(t => t.Name)
        //        .OrderBy(t => t.Name)
        //        .Skip((query.PageIndex - 1) * query.PageSize)
        //        .Take(query.PageSize)
        //        .ToList();
        //}

        public IQueryable<Tenant> Handle(GetCurrentTenantsQuery query)
        {
            return db.Tenants
                .AsNoTracking()
                //.OrderByDescending(t => t.Name)
                .OrderBy(t => t.Name)
                .Skip((query.PageIndex - 1) * query.PageSize)
                .Take(query.PageSize);
        }

        public Tenant Handle(GetTenantByIdQuery query)
        {
            // If the return type was/is IQueryable<AllTenants>
            //return
            //    from tenant in context.AllTenants
            //    where tenant.TenantId == query.TenantId
            //    select tenant;

            return db.Tenants
                .Find(query.TenantId);
        }
    }
}

