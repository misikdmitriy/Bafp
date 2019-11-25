using System.Threading.Tasks;
using AutoMapper;
using Bafp.Contracts.Database;
using Bafp.Logic.Models;
using Bafp.Logic.Services;
using Bafp.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bafp.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : SpControllerBase
    {
        public CitiesController(IDatabaseService databaseService, IMapper mapper) : base(databaseService, mapper)
        {
        }

        [HttpGet]
        [Route("")]
        public Task<GetAllCitiesResponse> GetAll() => ExecuteSp<CityDto, GetAllCitiesResponse>(new GetAllCitiesRequest());

        [HttpPut]
        [Route("course")]
        public Task<AddCityCourseResponse> AddCityCourse(AddCityCourseRequest request) => ExecuteSp<Null, AddCityCourseResponse>(request);

        [HttpPut]
        [Route("")]
        public Task<InsertNewCityResponse> Insert(InsertNewCityRequest request) => ExecuteSp<Null, InsertNewCityResponse>(request);
    }
}
