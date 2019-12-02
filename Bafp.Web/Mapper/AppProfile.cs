using System;
using System.Linq;
using AutoMapper;
using Bafp.Contracts;
using Bafp.Contracts.Database;
using Bafp.Contracts.Models;
using Bafp.Logic.Extensions;
using Bafp.Web.Models;

namespace Bafp.Web.Mapper
{
    public class AppProfile : Profile
    {
        public AppProfile()
        {
            CreateMap<GetCitiesRequest, DbRequest>()
                .BeforeMap((s, d) => d.ProcedureName = Constants.StoredProcedureNames.GetAllCities)
                .ForMember(dest => dest.ParameterResolver, src => src.MapFrom(x => new Func<object>(() => new
                {
                    CityIds = x.Ids.Select(id => new IdItem {Id = id}).AsTableValued()
                })));

            CreateMap<DbResponse<CityDto[]>, GetCitiesResponse>()
                .ForMember(dest => dest.Cities, src => src.MapFrom(x => x.Result));

            CreateMap<UpsertNewCityRequest, DbRequest>()
                .BeforeMap((s, d) => d.ProcedureName = Constants.StoredProcedureNames.UpsertCity)
                .ForMember(dest => dest.ParameterResolver, src => src.MapFrom(x => new Func<object>(() => new
                {
                    x.City.Name,
                    x.City.CategoryId
                })));

            CreateMap<DbResponse<CityDto[]>, UpsertNewCityResponse>()
                .ForMember(dest => dest.City, src => src.MapFrom(x => x.Result.FirstOrDefault()));

            CreateMap<UpsertCityCourseRequest, DbRequest>()
                .BeforeMap((s, d) => d.ProcedureName = Constants.StoredProcedureNames.UpsertCityCourse)
                .ForMember(dest => dest.ParameterResolver, src => src.MapFrom(x => new Func<object>(() => new
                {
                    x.CityCourse.CityId,
                    x.CityCourse.Count,
                    x.CityCourse.CourseId
                })));

            CreateMap<DbResponse<CityCourseDto[]>, UpsertCityCourseResponse>()
                .ForMember(dest => dest.CityCourse, src => src.MapFrom(x => x.Result.FirstOrDefault()));

            CreateMap<GetCoursesRequest, DbRequest>()
                .BeforeMap((s, d) => d.ProcedureName = Constants.StoredProcedureNames.GetAllCourses)
                .ForMember(dest => dest.ParameterResolver, src => src.MapFrom(x => new Func<object>(() => new
                {
                    CourseIds = x.Ids.Select(id => new IdItem {Id = id}).AsTableValued()
                })));

            CreateMap<DbResponse<CourseDto[]>, GetCoursesResponse>()
                .ForMember(dest => dest.Courses, src => src.MapFrom(x => x.Result));

            CreateMap<InsertNewCourseRequest, DbRequest>()
                .BeforeMap((s, d) => d.ProcedureName = Constants.StoredProcedureNames.InsertCourse)
                .ForMember(dest => dest.ParameterResolver, src => src.MapFrom(x => new Func<object>(() => new
                {
                    x.Course.Name
                })));

            CreateMap<DbResponse<CourseDto[]>, InsertNewCourseResponse>()
                .ForMember(dest => dest.Course, src => src.MapFrom(x => x.Result.FirstOrDefault()));

            CreateMap<GetAllCityCoursesByCityRequest, DbRequest>()
                .BeforeMap((s, d) => d.ProcedureName = Constants.StoredProcedureNames.GetAllCityCourses)
                .ForMember(dest => dest.ParameterResolver, src => src.MapFrom(x => new Func<object>(() => new
                {
                    x.CityId
                })));

            CreateMap<DbResponse<CityCourseDto[]>, GetAllCityCoursesByCityResponse>()
                .ForMember(dest => dest.CityCourses, src => src.MapFrom(x => x.Result));

            CreateMap<GetAllCityCoursesByCourseRequest, DbRequest>()
                .BeforeMap((s, d) => d.ProcedureName = Constants.StoredProcedureNames.GetAllCityCoursesByCourse)
                .ForMember(dest => dest.ParameterResolver, src => src.MapFrom(x => new Func<object>(() => new
                {
                    x.CourseId
                })));

            CreateMap<DbResponse<CityCourseDto[]>, GetAllCityCoursesByCourseResponse>()
                .ForMember(dest => dest.CityCourses, src => src.MapFrom(x => x.Result));

            CreateMap<GetCoursesPricingRequest, DbRequest>()
                .BeforeMap((s, d) => d.ProcedureName = Constants.StoredProcedureNames.GetCoursePriceList)
                .ForMember(dest => dest.ParameterResolver, src => src.MapFrom(x => new Func<object>(() => new
                {
                    x.CategoryId
                })));

            CreateMap<DbResponse<CoursePricingDto[]>, GetCoursesPricingResponse>()
                .ForMember(dest => dest.CoursePriceList, src => src.MapFrom(x => x.Result));

            CreateMap<GetCoursesPricingByCourseRequest, DbRequest>()
                .BeforeMap((s, d) => d.ProcedureName = Constants.StoredProcedureNames.GetCoursePriceListByCourse)
                .ForMember(dest => dest.ParameterResolver, src => src.MapFrom(x => new Func<object>(() => new
                {
                    x.CourseId
                })));

            CreateMap<DbResponse<CoursePricingDto[]>, GetCoursesPricingByCourseResponse>()
                .ForMember(dest => dest.CoursePriceList, src => src.MapFrom(x => x.Result));

            CreateMap<GetPricingCategoriesRequest, DbRequest>()
                .BeforeMap((s, d) => d.ProcedureName = Constants.StoredProcedureNames.GetAllPricingCategories)
                .ForMember(dest => dest.ParameterResolver, src => src.MapFrom(x => new Func<object>(() => new
                {
                    CategoryIds = x.Ids.Select(id => new IdItem {Id = id}).AsTableValued()
                })));

            CreateMap<DbResponse<PricingCategoryDto[]>, GetPricingCategoriesResponse>()
                .ForMember(dest => dest.Categories, src => src.MapFrom(x => x.Result));

            CreateMap<UpsertCoursePricingRequest, DbRequest>()
                .BeforeMap((s, d) => d.ProcedureName = Constants.StoredProcedureNames.UpsertCoursePricing)
                .ForMember(dest => dest.ParameterResolver, src => src.MapFrom(x => new Func<object>(() => new
                {
                    x.CoursePricing.CategoryId, x.CoursePricing.CourseId, x.CoursePricing.Price
                })));

            CreateMap<DbResponse<CoursePricingDto[]>, UpsertCoursePricingResponse>()
                .ForMember(dest => dest.CoursePricing, src => src.MapFrom(x => x.Result.FirstOrDefault()));

            CreateMap<DeleteCourseRequest, DbRequest>()
                .BeforeMap((s, d) => d.ProcedureName = Constants.StoredProcedureNames.DeleteCourse)
                .ForMember(dest => dest.ParameterResolver, src => src.MapFrom(x => new Func<object>(() => new
                {
                    x.CourseId
                })));

            CreateMap<DbResponse<CourseDto[]>, DeleteCourseResponse>()
                .ForMember(dest => dest.Course, src => src.MapFrom(x => x.Result.FirstOrDefault()));

            CreateMap<GetCitiesTotalRequest, DbRequest>()
                .BeforeMap((s, d) => d.ProcedureName = Constants.StoredProcedureNames.GetCitiesTotal)
                .ForMember(dest => dest.ParameterResolver, src => src.MapFrom(x => Defaults.ParameterResolver));

            CreateMap<DbResponse<CityTotalDto[]>, GetCitiesTotalResponse>()
                .ForMember(dest => dest.Total, src => src.MapFrom(x => x.Result));

            CreateMap<GetCityCoursesRequest, DbRequest>()
                .BeforeMap((s, d) => d.ProcedureName = Constants.StoredProcedureNames.GetCityCourses)
                .ForMember(dest => dest.ParameterResolver, src => src.MapFrom(x => Defaults.ParameterResolver));

            CreateMap<DbResponse<CityCourseDto[]>, GetCityCoursesResponse>()
                .ForMember(dest => dest.CityCourses, src => src.MapFrom(x => x.Result));
        }
    }
}