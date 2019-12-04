using AutoMapper;
using Bafp.Contracts.Models;
using Bafp.Web.Dto;

namespace Bafp.Web.Mapper
{
    public class ModelsProfile : Profile
    {
        public ModelsProfile()
        {
            CreateMap<City, CityDto>();
            CreateMap<Course, CourseDto>();
            CreateMap<CoursePricing, CoursePricingDto>();
            CreateMap<CityTotal, CityTotalDto>();
            CreateMap<CityCourse, CityCourseDto>();
            CreateMap<PricingCategory, PricingCategoryDto>();
        }
    }
}