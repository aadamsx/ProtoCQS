using AutoMapper;
using Proto.Domain.Commands.Tenants;
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

            Mapper.CreateMap<TenantViewModel, CreateOrUpdateTenantCommand>();
            //Mapper.CreateMap<CategoryFormModel, CreateOrUpdateCategoryCommand>();
            //Mapper.CreateMap<ExpenseFormModel, CreateOrUpdateExpenseCommand>();
            //Mapper.CreateMap<UserFormModel, UserRegisterCommand>();

            
        }
    }
}