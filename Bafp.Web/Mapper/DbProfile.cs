using System.Linq;
using AutoMapper;
using Bafp.Contracts;
using Bafp.Contracts.Database;
using Bafp.Contracts.Models;
using Bafp.Logic.Extensions;
using Bafp.Web.Models;

namespace Bafp.Web.Mapper
{
    public class DbProfile : Profile
    {
        public DbProfile()
        {
            CreateMap<GetCitiesRequest, DbRequest>()
                .BeforeMap((s, d) => d.ProcedureName = Constants.StoredProcedureNames.GetAllCities)
                .ForMember(dest => dest.Parameter, src => src.MapFrom(x => new
                {
                    CityIds = x.Ids.Select(id => new IdItem {Id = id}).AsTableValued()
                }));

            CreateMap<DbResponse<City[]>, GetCitiesResponse>()
                .ForMember(dest => dest.Cities, src => src.MapFrom(x => x.Result));

            CreateMap<UpsertNewCityRequest, DbRequest>()
                .BeforeMap((s, d) => d.ProcedureName = Constants.StoredProcedureNames.UpsertCity)
                .ForMember(dest => dest.Parameter, src => src.MapFrom(x => new
                {
                    x.City.Name,
                    x.City.CategoryId
                }));

            CreateMap<DbResponse<City[]>, UpsertNewCityResponse>()
                .ForMember(dest => dest.City, src => src.MapFrom(x => x.Result.FirstOrDefault()));

            CreateMap<UpsertCityCourseRequest, DbRequest>()
                .BeforeMap((s, d) => d.ProcedureName = Constants.StoredProcedureNames.UpsertCityCourse)
                .ForMember(dest => dest.Parameter, src => src.MapFrom(x => new
                {
                    x.CityCourse.CityId,
                    x.CityCourse.Count,
                    x.CityCourse.CourseId
                }));

            CreateMap<DbResponse<CityCourse[]>, UpsertCityCourseResponse>()
                .ForMember(dest => dest.CityCourse, src => src.MapFrom(x => x.Result.FirstOrDefault()));

            CreateMap<GetCoursesRequest, DbRequest>()
                .BeforeMap((s, d) => d.ProcedureName = Constants.StoredProcedureNames.GetAllCourses)
                .ForMember(dest => dest.Parameter, src => src.MapFrom(x => new
                {
                    CourseIds = x.Ids.Select(id => new IdItem {Id = id}).AsTableValued()
                }));

            CreateMap<DbResponse<Course[]>, GetCoursesResponse>()
                .ForMember(dest => dest.Courses, src => src.MapFrom(x => x.Result));

            CreateMap<InsertNewCourseRequest, DbRequest>()
                .BeforeMap((s, d) => d.ProcedureName = Constants.StoredProcedureNames.InsertCourse)
                .ForMember(dest => dest.Parameter, src => src.MapFrom(x => new
                {
                    x.Course.Name
                }));

            CreateMap<DbResponse<Course[]>, InsertNewCourseResponse>()
                .ForMember(dest => dest.Course, src => src.MapFrom(x => x.Result.FirstOrDefault()));

            CreateMap<GetAllCityCoursesByCityRequest, DbRequest>()
                .BeforeMap((s, d) => d.ProcedureName = Constants.StoredProcedureNames.GetAllCityCourses)
                .ForMember(dest => dest.Parameter, src => src.MapFrom(x => new
                {
                    x.CityId
                }));

            CreateMap<DbResponse<CityCourse[]>, GetAllCityCoursesByCityResponse>()
                .ForMember(dest => dest.CityCourses, src => src.MapFrom(x => x.Result));

            CreateMap<GetAllCityCoursesByCourseRequest, DbRequest>()
                .BeforeMap((s, d) => d.ProcedureName = Constants.StoredProcedureNames.GetAllCityCoursesByCourse)
                .ForMember(dest => dest.Parameter, src => src.MapFrom(x => new
                {
                    x.CourseId
                }));

            CreateMap<DbResponse<CityCourse[]>, GetAllCityCoursesByCourseResponse>()
                .ForMember(dest => dest.CityCourses, src => src.MapFrom(x => x.Result));

            CreateMap<GetCoursesPricingRequest, DbRequest>()
                .BeforeMap((s, d) => d.ProcedureName = Constants.StoredProcedureNames.GetCoursePriceList)
                .ForMember(dest => dest.Parameter, src => src.MapFrom(x => new
                {
                    CategoryIds = x.Ids.Select(id => new IdItem{Id = id}).AsTableValued()
                }));

            CreateMap<DbResponse<CoursePricing[]>, GetCoursesPricingResponse>()
                .ForMember(dest => dest.CoursePriceList, src => src.MapFrom(x => x.Result));

            CreateMap<GetCoursesPricingByCourseRequest, DbRequest>()
                .BeforeMap((s, d) => d.ProcedureName = Constants.StoredProcedureNames.GetCoursePriceListByCourse)
                .ForMember(dest => dest.Parameter, src => src.MapFrom(x => new
                {
                    x.CourseId
                }));

            CreateMap<DbResponse<CoursePricing[]>, GetCoursesPricingByCourseResponse>()
                .ForMember(dest => dest.CoursePriceList, src => src.MapFrom(x => x.Result));

            CreateMap<GetPricingCategoriesRequest, DbRequest>()
                .BeforeMap((s, d) => d.ProcedureName = Constants.StoredProcedureNames.GetAllPricingCategories)
                .ForMember(dest => dest.Parameter, src => src.MapFrom(x => new
                {
                    CategoryIds = x.Ids.Select(id => new IdItem {Id = id}).AsTableValued()
                }));

            CreateMap<DbResponse<PricingCategory[]>, GetPricingCategoriesResponse>()
                .ForMember(dest => dest.Categories, src => src.MapFrom(x => x.Result));

            CreateMap<UpsertCoursePricingRequest, DbRequest>()
                .BeforeMap((s, d) => d.ProcedureName = Constants.StoredProcedureNames.UpsertCoursePricing)
                .ForMember(dest => dest.Parameter, src => src.MapFrom(x => new
                {
                    x.CoursePricing.CategoryId, x.CoursePricing.CourseId, x.CoursePricing.Price
                }));

            CreateMap<DbResponse<CoursePricing[]>, UpsertCoursePricingResponse>()
                .ForMember(dest => dest.CoursePricing, src => src.MapFrom(x => x.Result.FirstOrDefault()));

            CreateMap<DeleteCourseRequest, DbRequest>()
                .BeforeMap((s, d) => d.ProcedureName = Constants.StoredProcedureNames.DeleteCourse)
                .ForMember(dest => dest.Parameter, src => src.MapFrom(x => new
                {
                    x.CourseId
                }));

            CreateMap<DbResponse<Course[]>, DeleteCourseResponse>()
                .ForMember(dest => dest.Course, src => src.MapFrom(x => x.Result.FirstOrDefault()));

            CreateMap<GetCitiesTotalRequest, DbRequest>()
                .BeforeMap((s, d) => d.ProcedureName = Constants.StoredProcedureNames.GetCitiesTotal)
                .ForMember(dest => dest.Parameter, src => src.MapFrom(x => Defaults.Parameter));

            CreateMap<DbResponse<CityTotal[]>, GetCitiesTotalResponse>()
                .ForMember(dest => dest.Total, src => src.MapFrom(x => x.Result));

            CreateMap<GetCityCoursesRequest, DbRequest>()
                .BeforeMap((s, d) => d.ProcedureName = Constants.StoredProcedureNames.GetCityCourses)
                .ForMember(dest => dest.Parameter, src => src.MapFrom(x => Defaults.Parameter));

            CreateMap<DbResponse<CityCourse[]>, GetCityCoursesResponse>()
                .ForMember(dest => dest.CityCourses, src => src.MapFrom(x => x.Result));
        }
    }
}