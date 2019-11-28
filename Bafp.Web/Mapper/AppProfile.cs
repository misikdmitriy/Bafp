using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Bafp.Contracts;
using Bafp.Contracts.Database;
using Bafp.Logic.Models;
using Bafp.Web.Models;

namespace Bafp.Web.Mapper
{
    public class AppProfile : Profile
    {
        public AppProfile()
        {
            CreateMap<GetAllCitiesRequest, DbRequest>()
                .BeforeMap((s, d) => d.ProcedureName = Constants.StoredProcedureNames.GetAllCities)
                .ForMember(dest => dest.ParameterResolver, src => src.MapFrom(x => Defaults.DefaultParameterResolver));

            CreateMap<DbResponse<IEnumerable<CityDto>>, GetAllCitiesResponse>()
                .ForMember(dest => dest.Cities, src => src.MapFrom(x => x.Result));

            CreateMap<UpsertNewCityRequest, DbRequest>()
                .BeforeMap((s, d) => d.ProcedureName = Constants.StoredProcedureNames.UpsertCity)
                .ForMember(dest => dest.ParameterResolver, src => src.MapFrom(x => new Func<object>(() => new
                {
                    x.City.Name,
                    x.City.CategoryId
                })));

            CreateMap<DbResponse<IEnumerable<CityDto>>, UpsertNewCityResponse>()
                .ForMember(dest => dest.City, src => src.MapFrom(x => x.Result.FirstOrDefault()));

            CreateMap<UpsertCityCourseRequest, DbRequest>()
                .BeforeMap((s, d) => d.ProcedureName = Constants.StoredProcedureNames.UpsertCityCourse)
                .ForMember(dest => dest.ParameterResolver, src => src.MapFrom(x => new Func<object>(() => new
                {
                    x.CityCourse.CityId,
                    x.CityCourse.Count,
                    x.CityCourse.CourseId
                })));

            CreateMap<DbResponse<IEnumerable<CityCourseDto>>, UpsertCityCourseResponse>()
                .ForMember(dest => dest.CityCourse, src => src.MapFrom(x => x.Result.FirstOrDefault()));

            CreateMap<GetAllCoursesRequest, DbRequest>()
                .BeforeMap((s, d) => d.ProcedureName = Constants.StoredProcedureNames.GetAllCourses)
                .ForMember(dest => dest.ParameterResolver, src => src.MapFrom(x => Defaults.DefaultParameterResolver));

            CreateMap<DbResponse<IEnumerable<CourseDto>>, GetAllCoursesResponse>()
                .ForMember(dest => dest.Courses, src => src.MapFrom(x => x.Result));

            CreateMap<InsertNewCourseRequest, DbRequest>()
                .BeforeMap((s, d) => d.ProcedureName = Constants.StoredProcedureNames.InsertCourse)
                .ForMember(dest => dest.ParameterResolver, src => src.MapFrom(x => new Func<object>(() => new
                {
                    x.Course.Name
                })));

            CreateMap<DbResponse<IEnumerable<CourseDto>>, InsertNewCourseResponse>()
                .ForMember(dest => dest.Course, src => src.MapFrom(x => x.Result.FirstOrDefault()));

            CreateMap<GetAllCityCoursesByCityRequest, DbRequest>()
                .BeforeMap((s, d) => d.ProcedureName = Constants.StoredProcedureNames.GetAllCityCourses)
                .ForMember(dest => dest.ParameterResolver, src => src.MapFrom(x => new Func<object>(() => new
                {
                    x.CityId
                })));

            CreateMap<DbResponse<IEnumerable<CityCourseDto>>, GetAllCityCoursesByCityResponse>()
                .ForMember(dest => dest.CityCourses, src => src.MapFrom(x => x.Result));

            CreateMap<GetAllCityCoursesByCourseRequest, DbRequest>()
                .BeforeMap((s, d) => d.ProcedureName = Constants.StoredProcedureNames.GetAllCityCoursesByCourse)
                .ForMember(dest => dest.ParameterResolver, src => src.MapFrom(x => new Func<object>(() => new
                {
                    x.CourseId
                })));

            CreateMap<DbResponse<IEnumerable<CityCourseDto>>, GetAllCityCoursesByCourseResponse>()
                .ForMember(dest => dest.CityCourses, src => src.MapFrom(x => x.Result));

            CreateMap<GetCoursesPricingRequest, DbRequest>()
                .BeforeMap((s, d) => d.ProcedureName = Constants.StoredProcedureNames.GetCoursePriceList)
                .ForMember(dest => dest.ParameterResolver, src => src.MapFrom(x => new Func<object>(() => new
                {
                    x.CategoryId
                })));

            CreateMap<DbResponse<IEnumerable<CoursePricingDto>>, GetCoursesPricingResponse>()
                .ForMember(dest => dest.CoursePriceList, src => src.MapFrom(x => x.Result));

            CreateMap<GetCoursesPricingByCourseRequest, DbRequest>()
                .BeforeMap((s, d) => d.ProcedureName = Constants.StoredProcedureNames.GetCoursePriceListByCourse)
                .ForMember(dest => dest.ParameterResolver, src => src.MapFrom(x => new Func<object>(() => new
                {
                    x.CourseId
                })));

            CreateMap<DbResponse<IEnumerable<CoursePricingDto>>, GetCoursesPricingByCourseResponse>()
                .ForMember(dest => dest.CoursePriceList, src => src.MapFrom(x => x.Result));

            CreateMap<GetAllPricingCategoriesRequest, DbRequest>()
                .BeforeMap((s, d) => d.ProcedureName = Constants.StoredProcedureNames.GetAllPricingCategories)
                .ForMember(dest => dest.ParameterResolver, src => src.MapFrom(x => Defaults.DefaultParameterResolver));

            CreateMap<DbResponse<IEnumerable<PricingCategoryDto>>, GetAllPricingCategoriesResponse>()
                .ForMember(dest => dest.Categories, src => src.MapFrom(x => x.Result));

            CreateMap<UpsertCoursePricingRequest, DbRequest>()
                .BeforeMap((s, d) => d.ProcedureName = Constants.StoredProcedureNames.UpsertCoursePricing)
                .ForMember(dest => dest.ParameterResolver, src => src.MapFrom(x => new Func<object>(() => new
                {
                    x.CoursePricing.CategoryId, x.CoursePricing.CourseId, x.CoursePricing.Price
                })));

            CreateMap<DbResponse<IEnumerable<CoursePricingDto>>, UpsertCoursePricingResponse>()
                .ForMember(dest => dest.CoursePricing, src => src.MapFrom(x => x.Result.FirstOrDefault()));

            CreateMap<DeleteCourseRequest, DbRequest>()
                .BeforeMap((s, d) => d.ProcedureName = Constants.StoredProcedureNames.DeleteCourse)
                .ForMember(dest => dest.ParameterResolver, src => src.MapFrom(x => new Func<object>(() => new
                {
                    x.CourseId
                })));

            CreateMap<DbResponse<IEnumerable<CourseDto>>, DeleteCourseResponse>()
                .ForMember(dest => dest.Course, src => src.MapFrom(x => x.Result.FirstOrDefault()));
        }
    }
}
