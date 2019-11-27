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
        public Task<IActionResult> GetAllCities() => ExecuteSp<CityDto, GetAllCitiesResponse>(new GetAllCitiesRequest());


        [HttpGet]
        [Route("{cityId}/courses")]
        public Task<IActionResult> GetAllCityCourses(int cityId) => ExecuteSp<CityCourseDto, GetAllCityCoursesResponse>(new GetAllCityCoursesRequest { CityId = cityId });

        [HttpPut]
        [Route("courses")]
        public Task<IActionResult> UpsertCityCourse(UpsertCityCourseRequest request) => ExecuteSp<Null, UpsertCityCourseResponse>(request);

        [HttpPut]
        [Route("")]
        public Task<IActionResult> UpsertCity(UpsertNewCityRequest request) => ExecuteSp<Null, UpsertNewCityResponse>(request);
    }
}
