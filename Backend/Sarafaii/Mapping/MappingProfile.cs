using AutoMapper;
using Sarafaii.DTOs;
using Sarafaii.Models;

namespace Sarafaii.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Ledger, LedgerResponse>()
            .ForMember(dest => dest.ToCustomer, opt => opt.MapFrom(src => src.ToCustomer))
            .ForMember(dest => dest.FromCustomer, opt => opt.MapFrom(src => src.FromCustomer))
            .ForMember(dest => dest.Currency, opt => opt.MapFrom(src => src.Currency.Name));

        CreateMap<Customer, CustomerDto>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FirstName + " " + src.LastName))
            .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth))
            .ForMember(dest => dest.ProfilePictureUrl, opt => opt.MapFrom(src => src.ProfilePictureUrl))
            .ForMember(dest => dest.BirthCertificateUrl, opt => opt.MapFrom(src => src.BirthCertificateUrl));

    }
}