using System.Collections.Generic;
using AutoMapper;
using dm = DataModel;
using vm = ViewModel;

namespace ModelMap
{
    public static class ModelExtensions
    {
        public static vm.Tenant ToModel(this dm.Tenant model) 
        {
            return Mapper.Map<dm.Tenant, vm.Tenant>(model);
        }

        public static dm.Tenant ToModel(this vm.Tenant model)
        {
            return Mapper.Map<vm.Tenant, dm.Tenant>(model);
        }

        public static IEnumerable<vm.Tenant> ToModel(this IEnumerable<dm.Tenant> models)
        {
            return Mapper.Map<IEnumerable<dm.Tenant>, IEnumerable<vm.Tenant>>(models);
        }

        //public static TModel ToModel<TModel>(this IEntity entity) where TModel : IBaseViewModel
        //{
        //    return (TModel)Mapper.Map(entity, entity.GetType(), typeof(TModel));
        //}
    }
}

/*
 */ 

//Then the code is still shorted than it was:

//var city = GetCity(Id);
//var model = city.ToModel<CityModel>();

