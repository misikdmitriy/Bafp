using System;
using System.Collections.Generic;
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
                    x.City.Name, x.City.CategoryId
                })));

            CreateMap<DbResponse<IEnumerable<Null>>, UpsertNewCityResponse>();

            CreateMap<UpsertCityCourseRequest, DbRequest>()
                .BeforeMap((s, d) => d.ProcedureName = Constants.StoredProcedureNames.UpsertCityCourse)
                .ForMember(dest => dest.ParameterResolver, src => src.MapFrom(x => new Func<object>(() => new
                {
                    x.CityId, x.Count, x.CourseId
                })));

            CreateMap<DbResponse<IEnumerable<Null>>, UpsertCityCourseResponse>();

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

            CreateMap<DbResponse<IEnumerable<Null>>, InsertNewCourseResponse>();

            CreateMap<GetAllCityCoursesRequest, DbRequest>()
                .BeforeMap((s, d) => d.ProcedureName = Constants.StoredProcedureNames.GetAllCityCourses)
                .ForMember(dest => dest.ParameterResolver, src => src.MapFrom(x => new Func<object>(() => new
                {
                    x.CityId
                })));

            CreateMap<DbResponse<IEnumerable<CityCourseDto>>, GetAllCityCoursesResponse>()
                .ForMember(dest => dest.CityCourses, src => src.MapFrom(x => x.Result));

            CreateMap<GetCoursesPricingRequest, DbRequest>()
                .BeforeMap((s, d) => d.ProcedureName = Constants.StoredProcedureNames.GetCoursePriceList)
                .ForMember(dest => dest.ParameterResolver, src => src.MapFrom(x => new Func<object>(() => new
                {
                    x.CategoryId
                })));

            CreateMap<DbResponse<IEnumerable<CoursePricingDto>>, GetCoursesPricingResponse>()
                .ForMember(dest => dest.CoursePriceList, src => src.MapFrom(x => x.Result));
        }
    }
}
