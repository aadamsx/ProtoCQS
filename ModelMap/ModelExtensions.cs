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
            return (ViewModel.Tenant)Mapper.Map(model, model.GetType(), typeof(ViewModel.Tenant));
        }

        public static DataModel.Tenant ToModel(this ViewModel.Tenant model)
        {
            return (DataModel.Tenant)Mapper.Map(model, model.GetType(), typeof(DataModel.Tenant));
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

