using AutoMapper;

namespace IdentityClient.Infrastructure.RelationDatabase
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<Core.Models.User, User>()
                .ForMember(u=>u.SecurityStamp , x=> x.Ignore())
                .ReverseMap();
            CreateMap<Core.Models.Address, Address>().ReverseMap();
        }
    }
}
