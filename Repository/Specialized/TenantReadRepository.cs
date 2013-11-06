using System.Collections.Generic;
using System.Linq;
using Data;
using DataModel.Services;
using ViewModel;
using ContactType = DataModel.ContactType;
using Tenant = DataModel.Tenant;

namespace Repository.Specialized
{
    //public class TenantReadRepository
    //    : ReadRepository<Tenant>
    //    , ITenantReadRepository
    //{
    //    public TenantReadRepository(
    //        ClientManagementContext context) 
    //        : base(context) { }


    //    public IEnumerable<ContactType> GetContactTypes()
    //    {
    //        return Context
    //            .Set<ContactType>()
    //            .AsNoTracking()
    //            .AsEnumerable();
    //    }
    //}
}