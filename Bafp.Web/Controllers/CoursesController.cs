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
    public class CoursesController : SpControllerBase
    {
        public CoursesController(IDatabaseService databaseService, IMapper mapper) : base(databaseService, mapper,
            Log.ForContext<CoursesController>())
        {
        }

        [HttpGet]
        [Route("")]
        public Task<IActionResult> GetAllCourses() =>
            ExecuteSp<CourseDto, GetAllCoursesResponse>(new GetAllCoursesRequest());

        [HttpGet]
        [Route("prices/{categoryId}")]
        public Task<IActionResult> GetCoursesPricing(int categoryId) =>
            ExecuteSp<CoursePricingDto, GetCoursesPricingResponse>(new GetCoursesPricingRequest
                {CategoryId = categoryId});

        [HttpGet]
        [Route("{courseId}/prices")]
        public Task<IActionResult> GetCoursesPricingByCourse(int courseId) =>
            ExecuteSp<CoursePricingDto, GetCoursesPricingByCourseResponse>(new GetCoursesPricingByCourseRequest
                {CourseId = courseId});

        [HttpPut]
        [Route("prices")]
        public Task<IActionResult> UpsertCoursePricing(UpsertCoursePricingRequest request) =>
            ExecuteSp<CoursePricingDto, UpsertCoursePricingResponse>(request);

        [HttpPut]
        [Route("")]
        public Task<IActionResult> UpsertCourse(InsertNewCourseRequest request) =>
            ExecuteSp<CourseDto, InsertNewCourseResponse>(request);

        [HttpDelete]
        [Route("{courseId}")]
        public Task<IActionResult> DeleteCourse(int courseId) =>
            ExecuteSp<CourseDto, DeleteCourseResponse>(new DeleteCourseRequest {CourseId = courseId});
    }
}