using AutoMapper;
using Proto.Model.Entities;
using Proto.Mvc.Mgmt.Models;

namespace Proto.Mvc.Mgmt.Mappers
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "ViewModelToDomainMappings"; }
        }

        protected override void Configure()
        {
            //Mapper.CreateMap<CurrentTenantsViewModel, CurrentTenants>(); //.ForMember(dest => dest.Category, opt => opt.Ignore());

            Mapper.CreateMap<TenantViewModel, Tenant>()
                .ForMember(dest => dest.Type, opt => opt.Ignore());
            //Mapper.CreateMap<CategoryFormModel, CreateOrUpdateCategoryCommand>();
            //Mapper.CreateMap<ExpenseFormModel, CreateOrUpdateExpenseCommand>();
            //Mapper.CreateMap<UserFormModel, UserRegisterCommand>();

        }
    }
}