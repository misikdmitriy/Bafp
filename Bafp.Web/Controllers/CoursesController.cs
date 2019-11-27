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
    public class CoursesController : SpControllerBase
    {
        public CoursesController(IDatabaseService databaseService, IMapper mapper) : base(databaseService, mapper)
        {
        }

        [HttpGet]
        [Route("")]
        public Task<IActionResult> GetAllCourses() => ExecuteSp<CourseDto, GetAllCoursesResponse>(new GetAllCoursesRequest());

        [HttpGet]
        [Route("prices/{categoryId}")]
        public Task<IActionResult> GetCoursesPricing(int categoryId) => ExecuteSp<CoursePricingDto, GetCoursesPricingResponse>(new GetCoursesPricingRequest { CategoryId = categoryId });

        [HttpPut]
        [Route("prices")]
        public Task<IActionResult> UpsertCoursePricing(UpsertCoursePricingRequest request) => ExecuteSp<Null, UpsertCoursePricingResponse>(request);

        [HttpPut]
        [Route("")]
        public Task<IActionResult> UpsertCourse(InsertNewCourseRequest request) => ExecuteSp<Null, InsertNewCourseResponse>(request);
    }
}