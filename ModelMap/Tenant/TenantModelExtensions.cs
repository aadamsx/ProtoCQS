using System.Collections.Generic;
using AutoMapper;
using dm = DataModel;
using vm = ViewModel;

namespace ModelMap.Tenant
{
    public static class TenantModelExtensions
    {
        // Tenant
        public static vm.Tenant ToModel(this dm.Tenant model)
        {
            return Mapper.Map<dm.Tenant, vm.Tenant>(model);
        }

        public static dm.Tenant ToModel(this vm.Tenant model)
        {
            return Mapper.Map<vm.Tenant, dm.Tenant>(model);
        }

        public static IEnumerable<vm.Tenant> ToModels(this IEnumerable<dm.Tenant> models)
        {
            return Mapper.Map<IEnumerable<dm.Tenant>, IEnumerable<vm.Tenant>>(models);
        }

        // ContactTypes
        public static IEnumerable<vm.ContactType> ToModels(this IEnumerable<dm.ContactType> models)
        {
            return Mapper.Map<IEnumerable<dm.ContactType>, IEnumerable<vm.ContactType>>(models);
        }

    }

}
