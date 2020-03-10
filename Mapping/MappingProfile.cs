using System.Collections.Generic;
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
            CreateMap<Vehicle, VehicleResource>()
                .ForMember(
                    dest => dest.Contact,
                    opt => opt.MapFrom(
                        src => new ContactResource 
                        {
                            Name = src.ContactName,
                            Email = src.ContactEmail,
                            Mobile = src.ContactMobile
                        }
                    )
                )
                .ForMember(
                    dest => dest.Features,
                    opt => opt.MapFrom(
                        src => src.VehicleFeatures.Select(vf => vf.FeatureId)
                    )
                );

            // API Resource to Domain
            CreateMap<VehicleResource, Vehicle>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.ContactName, opt => opt.MapFrom(src => src.Contact.Name))
                .ForMember(dest => dest.ContactEmail, opt => opt.MapFrom(src => src.Contact.Email))
                .ForMember(dest => dest.ContactMobile, opt => opt.MapFrom(src => src.Contact.Mobile))
                .ForMember(dest => dest.VehicleFeatures, opt => opt.Ignore())
                .AfterMap((src, dest) => {

                    var removedFeatures = dest.VehicleFeatures
                    .Where(vf => !src.Features.Contains(vf.FeatureId));

                    foreach (var vf in removedFeatures)
                        dest.VehicleFeatures.Remove(vf);

                    var addedFeatures = src.Features
                    .Where(fId => !dest.VehicleFeatures.Any(vf => vf.FeatureId == fId))
                    .Select(fId => new VehicleFeature { FeatureId = fId });

                    foreach (var vf in addedFeatures)
                        dest.VehicleFeatures.Add(vf);
                    
                });

        }

    }
}