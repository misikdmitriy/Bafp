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
            CreateMap<GetAllCitiesRequest, Request>()
                .BeforeMap((s, d) => d.ProcedureName = Constants.StoredProcedureNames.GetAllCities)
                .ForMember(dest => dest.ParameterResolver, src => src.MapFrom(x => Defaults.DefaultParameterResolver));

            CreateMap<Response<IEnumerable<CityDto>>, GetAllCitiesResponse>()
                .ForMember(dest => dest.Cities, src => src.MapFrom(x => x.Result));

            CreateMap<InsertNewCityRequest, Request>()
                .BeforeMap((s, d) => d.ProcedureName = Constants.StoredProcedureNames.InsertCity)
                .ForMember(dest => dest.ParameterResolver, src => src.MapFrom(x => new Func<object>(() => new
                {
                    x.City.CityName, x.City.CategoryName
                })));

            CreateMap<Response<IEnumerable<Null>>, InsertNewCityResponse>();
        }
    }
}
