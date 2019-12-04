using System.Threading.Tasks;
using AutoMapper;
using Bafp.Contracts;
using Bafp.Contracts.Models;
using Bafp.Logic.Services;
using Bafp.Web.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Bafp.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors(Constants.PolicyNames.AllowUi)]
    public class CitiesController : SpControllerBase
    {
        public CitiesController(IDatabaseService databaseService, IMapper mapper) : base(databaseService, mapper,
            Log.ForContext<CitiesController>())
        {
        }

        [HttpPost]
        [Route("")]
        public Task<IActionResult> GetAllCities(GetCitiesRequest request) =>
            ExecuteSp<City, GetCitiesResponse>(request ?? new GetCitiesRequest());


        [HttpGet]
        [Route("{cityId}/courses")]
        public Task<IActionResult> GetAllCityCoursesByCity(int cityId) =>
            ExecuteSp<CityCourse, GetAllCityCoursesByCityResponse>(new GetAllCityCoursesByCityRequest
                {CityId = cityId});

        [HttpGet]
        [Route("courses/{courseId}")]
        public Task<IActionResult> GetAllCityCoursesByCourse(int courseId) =>
            ExecuteSp<CityCourse, GetAllCityCoursesByCourseResponse>(new GetAllCityCoursesByCourseRequest
                {CourseId = courseId});
        
        [HttpGet]
        [Route("courses")]
        public Task<IActionResult> GetCityCourses() =>
            ExecuteSp<CityCourse, GetCityCoursesResponse>(new GetCityCoursesRequest());

        [HttpPut]
        [Route("courses")]
        public Task<IActionResult> UpsertCityCourse(UpsertCityCourseRequest request) =>
            ExecuteSp<CityCourse, UpsertCityCourseResponse>(request);

        [HttpPut]
        [Route("")]
        public Task<IActionResult> UpsertCity(UpsertNewCityRequest request) =>
            ExecuteSp<City, UpsertNewCityResponse>(request);

        [HttpGet]
        [Route("total")]
        public Task<IActionResult> GetCitiesTotal() =>
            ExecuteSp<CityTotal, GetCitiesTotalResponse>(new GetCitiesTotalRequest());
    }
}