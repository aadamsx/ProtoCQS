using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace ModelMap
{
    public static class ModelExtensions
    {
        public static ViewModel.Tenant ToModel(this DataModel.Tenant model) 
        {
            return Mapper.Map<DataModel.Tenant, ViewModel.Tenant>(model);
        }

        public static DataModel.Tenant ToModel(this ViewModel.Tenant model)
        {
            return Mapper.Map<ViewModel.Tenant, DataModel.Tenant>(model);
        }

        public static IEnumerable<ViewModel.Tenant> ToModel(this IEnumerable<DataModel.Tenant> models)
        {
            return Mapper.Map<IEnumerable<DataModel.Tenant>, IEnumerable<ViewModel.Tenant>>(models);
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

