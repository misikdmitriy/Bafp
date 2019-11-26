using System.Reflection.Metadata;
using System.Threading.Tasks;
using AutoMapper;
using Bafp.Contracts;
using Bafp.Contracts.Database;
using Bafp.Logic.Models;
using Bafp.Logic.Services;
using Bafp.Web.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Bafp.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors(Constants.PolicyNames.AllowUi)]
    public class CitiesController : SpControllerBase
    {
        public CitiesController(IDatabaseService databaseService, IMapper mapper) : base(databaseService, mapper)
        {
        }

        [HttpGet]
        [Route("")]
        public Task<GetAllCitiesResponse> GetAll() => ExecuteSp<CityDto, GetAllCitiesResponse>(new GetAllCitiesRequest());


        [HttpGet]
        [Route("{cityName}/courses")]
        public Task<GetAllCityCoursesResponse> GetAllCityCourses(string cityName) => ExecuteSp<CityCourseDto, GetAllCityCoursesResponse>(new GetAllCityCoursesRequest { CityName = cityName });

        [HttpPut]
        [Route("courses")]
        public Task<AddCityCourseResponse> AddCityCourse(AddCityCourseRequest request) => ExecuteSp<Null, AddCityCourseResponse>(request);

        [HttpPut]
        [Route("")]
        public Task<InsertNewCityResponse> Insert(InsertNewCityRequest request) => ExecuteSp<Null, InsertNewCityResponse>(request);
    }
}
