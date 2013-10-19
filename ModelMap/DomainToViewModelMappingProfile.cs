using AutoMapper;

namespace ModelMap
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "DomainToViewModelMappings"; }
        }

        protected override void Configure()
        {
            //Mapper.CreateMap<CurrentTenantsViewModel, CurrentTenants>(); //.ForMember(dest => dest.Category, opt => opt.Ignore());
            //Mapper.CreateMap<AllTenants, TenantDetailViewModel>();
            Mapper.CreateMap<DataModel.Tenant, ViewModel.TenantViewModel>();
        }
    }
}