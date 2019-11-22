using System.Collections.Generic;
using AutoMapper;
using Bafp.Contracts.Database;
using Bafp.Web.Models;

namespace Bafp.Web.Mapper
{
    public class AppProfile : Profile
    {
        public AppProfile()
        {
            CreateMap<GetAllCitiesRequest, Request>()
                .BeforeMap((s, d) => d.ProcedureName = "dbo.GetAllCities");

            CreateMap<Response<IEnumerable<CityDto>>, GetAllCitiesResponse>()
                .ForMember(dest => dest.Success, src => src.MapFrom(x => x.Success))
                .ForMember(dest => dest.Message, src => src.MapFrom(x => x.Message))
                .ForMember(dest => dest.Cities, src => src.MapFrom(x => x.Result));
        }
    }
}
