using System.Collections.Generic;
using AutoMapper;
using dm = DataModel;
using vm = ViewModel;

namespace ModelMap
{
    public static class ModelExtensions
    {


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

