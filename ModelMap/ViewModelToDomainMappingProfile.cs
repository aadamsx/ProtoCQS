using AutoMapper;
using dm = DataModel;
using vm = ViewModel;

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


            Mapper.CreateMap<vm.ContactType, dm.ContactType>()
                .ForMember(dest => dest.Tenants, opt => opt.Ignore());
            Mapper.CreateMap<vm.Tenant, dm.Tenant>();
            //.ForMember(dest => dest.Type, opt => opt.Ignore());

            
            //Mapper.CreateMap<CategoryFormModel, CreateOrUpdateCategoryCommand>();
            //Mapper.CreateMap<ExpenseFormModel, CreateOrUpdateExpenseCommand>();
            //Mapper.CreateMap<UserFormModel, UserRegisterCommand>();

        }
    }
}