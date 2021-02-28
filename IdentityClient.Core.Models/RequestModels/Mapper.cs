using AutoMapper;
using IdentityModel;
using System;

namespace IdentityClient.Core.Models.RequestModels
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<RegisterUserRequest, User>()
                .ForMember(u => u.Id, opt => opt.MapFrom(r => Guid.NewGuid()))
                .ForMember(u => u.ResidentialAddress, opt => opt.MapFrom(r => r.Address))
                .ForMember(u => u.PasswordHash, opt => opt.MapFrom(r => r.Password.ToSha256()));

            CreateMap<EditUserRequest, User>().ForMember(u => u.ResidentialAddress, opt => opt.MapFrom(r => r.Address));
            CreateMap<Guid, User>().ForMember(u => u.Id, opt => opt.MapFrom(r => r));

            CreateMap<AddAddressRequest, Address>();
            CreateMap<EditAddressRequest, Address>();
        }

        
    }
}
