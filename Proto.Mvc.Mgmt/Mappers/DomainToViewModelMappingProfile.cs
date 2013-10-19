using AutoMapper;
using Proto.Mvc.Mgmt.Models;

namespace Proto.Mvc.Mgmt.Mappers
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
            Mapper.CreateMap<DataModel.Tenant, TenantViewModel>();
        }
    }
}