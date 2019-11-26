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

            CreateMap<InsertNewCityRequest, DbRequest>()
                .BeforeMap((s, d) => d.ProcedureName = Constants.StoredProcedureNames.InsertCity)
                .ForMember(dest => dest.ParameterResolver, src => src.MapFrom(x => new Func<object>(() => new
                {
                    x.City.Name, x.City.CategoryName
                })));

            CreateMap<DbResponse<IEnumerable<Null>>, InsertNewCityResponse>();

            CreateMap<AddCityCourseRequest, DbRequest>()
                .BeforeMap((s, d) => d.ProcedureName = Constants.StoredProcedureNames.AddCityCourse)
                .ForMember(dest => dest.ParameterResolver, src => src.MapFrom(x => new Func<object>(() => x)));

            CreateMap<DbResponse<IEnumerable<Null>>, AddCityCourseResponse>();

            CreateMap<GetAllCoursesRequest, DbRequest>()
                .BeforeMap((s, d) => d.ProcedureName = Constants.StoredProcedureNames.GetAllCourses)
                .ForMember(dest => dest.ParameterResolver, src => src.MapFrom(x => Defaults.DefaultParameterResolver));

            CreateMap<DbResponse<IEnumerable<CourseDto>>, GetAllCoursesResponse>()
                .ForMember(dest => dest.Courses, src => src.MapFrom(x => x.Result));

            CreateMap<InsertNewCourseRequest, DbRequest>()
                .BeforeMap((s, d) => d.ProcedureName = Constants.StoredProcedureNames.InsertCourse)
                .ForMember(dest => dest.ParameterResolver, src => src.MapFrom(x => new Func<object>(() => new
                {
                    CourseName = x.Course.Name
                })));

            CreateMap<DbResponse<IEnumerable<Null>>, InsertNewCourseResponse>();

            CreateMap<GetAllCityCoursesRequest, DbRequest>()
                .BeforeMap((s, d) => d.ProcedureName = Constants.StoredProcedureNames.GetAllCityCourses)
                .ForMember(dest => dest.ParameterResolver, src => src.MapFrom(x => new Func<object>(() => new
                {
                    x.CityName
                })));

            CreateMap<DbResponse<IEnumerable<CityCourseDto>>, GetAllCityCoursesResponse>()
                .ForMember(dest => dest.CityCourses, src => src.MapFrom(x => x.Result));
        }
    }
}
