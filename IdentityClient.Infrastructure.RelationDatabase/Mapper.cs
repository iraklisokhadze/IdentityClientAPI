using AutoMapper;

namespace IdentityClient.Infrastructure.RelationDatabase
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<Core.Models.User, User>()
                .ForMember(u => u.SecurityStamp, x => x.Ignore())
                .ForMember(u => u.EmailConfirmed, x => x.MapFrom(a => true))
                .ForMember(u => u.PhoneNumberConfirmed, x => x.MapFrom(a => true))
                .ReverseMap();
            CreateMap<Core.Models.Address, Address>().ReverseMap();
        }
    }
}
