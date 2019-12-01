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

        [HttpGet]
        [Route("")]
        public Task<IActionResult> GetAllCities() =>
            ExecuteSp<CityDto, GetAllCitiesResponse>(new GetAllCitiesRequest());


        [HttpGet]
        [Route("{cityId}/courses")]
        public Task<IActionResult> GetAllCityCoursesByCity(int cityId) =>
            ExecuteSp<CityCourseDto, GetAllCityCoursesByCityResponse>(new GetAllCityCoursesByCityRequest
                {CityId = cityId});

        [HttpGet]
        [Route("courses/{courseId}")]
        public Task<IActionResult> GetAllCityCoursesByCourse(int courseId) =>
            ExecuteSp<CityCourseDto, GetAllCityCoursesByCourseResponse>(new GetAllCityCoursesByCourseRequest
                {CourseId = courseId});
        
        [HttpGet]
        [Route("courses")]
        public Task<IActionResult> GetCityCourses() =>
            ExecuteSp<CityCourseDto, GetCityCoursesResponse>(new GetCityCoursesRequest());

        [HttpPut]
        [Route("courses")]
        public Task<IActionResult> UpsertCityCourse(UpsertCityCourseRequest request) =>
            ExecuteSp<CityCourseDto, UpsertCityCourseResponse>(request);

        [HttpPut]
        [Route("")]
        public Task<IActionResult> UpsertCity(UpsertNewCityRequest request) =>
            ExecuteSp<CityDto, UpsertNewCityResponse>(request);

        [HttpGet]
        [Route("total")]
        public Task<IActionResult> GetCitiesTotal() =>
            ExecuteSp<CityTotalDto, GetCitiesTotalResponse>(new GetCitiesTotalRequest());
    }
}