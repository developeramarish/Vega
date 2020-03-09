using System.Linq;
using AutoMapper;
using Vega.Controllers.Resources;
using Vega.Models;

namespace Vega.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain to API resources
            CreateMap<Make, MakeResource>();
            CreateMap<Model, ModelResource>();
            CreateMap<Feature, FeatureResource>();

            // API Resource to Domain
            CreateMap<VehicleResource, Vehicle>()
                .ForMember(dest => dest.ContactName, opt => opt.MapFrom(src => src.Contact.Name))
                .ForMember(dest => dest.ContactEmail, opt => opt.MapFrom(src => src.Contact.Email))
                .ForMember(dest => dest.ContactMobile, opt => opt.MapFrom(src => src.Contact.Mobile))
                .ForMember(dest => dest.VehicleFeatures, opt => opt.MapFrom(src => src.VehicleFeatures.Select(vfId => new VehicleFeature { FeatureId = vfId })));

        }
        
    }
}