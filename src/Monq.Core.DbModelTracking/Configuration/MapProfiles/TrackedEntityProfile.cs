using AutoMapper;
using Monq.Core.DbModelTracking.Models;

namespace Monq.Core.DbModelTracking.Configuration.MapProfiles
{
    public class TrackedEntityProfile : Profile
    {
        public TrackedEntityProfile()
        {
            CreateMap<TrackedEntity, TrackedEntityViewModel>()
                .ForMember(x => x.CreatedAt, m => m.MapFrom(p => p.CreatedAt.ToUnixTimeSeconds()))
                .ForMember(x => x.CreatedBy, m => m.MapFrom(p => p.CreatedBy))
                .ForMember(x => x.CreatedByName,
                    m => m.MapFrom(p => p.CreatedBy != null ? p.CreatedByName : $"{p.CreatedByName} (удален)"))
                .ForMember(x => x.UpdatedAt,
                    m => m.MapFrom(p => p.UpdatedAt != null ? (long?)p.UpdatedAt.Value.ToUnixTimeSeconds() : null))
                .ForMember(x => x.UpdatedBy, m => m.MapFrom(p => p.UpdatedBy))
                .ForMember(x => x.UpdatedByName,
                    m => m.MapFrom(p => p.UpdatedBy != null ? p.UpdatedByName : $"{p.UpdatedByName} (удален)"));

            CreateMap<TrackedEntity, Models.v2.TrackedEntityViewModel>()
                .ForMember(x => x.CreatedAt, m => m.MapFrom(p => p.CreatedAt))
                .ForMember(x => x.CreatedBy, m => m.MapFrom(p => p.CreatedBy))
                .ForMember(x => x.CreatedByName, m => m.MapFrom(p => p.CreatedBy != null ? p.CreatedByName : $"{p.CreatedByName} (удален)"))
                .ForMember(x => x.UpdatedAt, m => m.MapFrom(p => p.UpdatedAt))
                .ForMember(x => x.UpdatedBy, m => m.MapFrom(p => p.UpdatedBy))
                .ForMember(x => x.UpdatedByName, m => m.MapFrom(p => p.UpdatedBy != null ? p.UpdatedByName : $"{p.UpdatedByName} (удален)"));
        }
    }
}
