using System.Linq;
using AutoMapper;
using DATINGAPP.API.Dtos;
using DATINGAPP.API.Helper;
using DATINGAPP.API.Models;

namespace DATINGAPP.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Photo, PhotosForDetailDto>();  

            CreateMap<User, UserForListDto>()
            .ForMember(dest => dest.PhotoUrl, opt => {
                opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url);
            })
            .ForMember(dest => dest.Age, opt => {
                opt.MapFrom(src => src.DateOfBirth.CalculateAge());
            });
            
            CreateMap<User, UserForDetailedDto>()
            .ForMember(dest => dest.Age, opt => {
                opt.MapFrom(src => src.DateOfBirth.CalculateAge());
            }).ForMember(dest => dest.PhotoUrl, opt => {
                opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url);
            });
        }
    }
}