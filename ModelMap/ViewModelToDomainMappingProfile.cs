using AutoMapper;

namespace ModelMap
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

            Mapper.CreateMap<ViewModel.TenantViewModel, DataModel.Tenant>()
                .ForMember(dest => dest.Type, opt => opt.Ignore());
            //Mapper.CreateMap<CategoryFormModel, CreateOrUpdateCategoryCommand>();
            //Mapper.CreateMap<ExpenseFormModel, CreateOrUpdateExpenseCommand>();
            //Mapper.CreateMap<UserFormModel, UserRegisterCommand>();

        }
    }
}