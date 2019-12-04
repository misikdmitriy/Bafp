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
    public class CoursesController : SpControllerBase
    {
        public CoursesController(IDatabaseService databaseService, IMapper mapper) : base(databaseService, mapper,
            Log.ForContext<CoursesController>())
        {
        }

        [HttpPost]
        [Route("")]
        public Task<IActionResult> GetCourses(GetCoursesRequest request) =>
            ExecuteSp<Course, GetCoursesResponse>(request ?? new GetCoursesRequest());

        [HttpPost]
        [Route("prices")]
        public Task<IActionResult> GetCoursesPricing(GetCoursesPricingRequest request) =>
            ExecuteSp<CoursePricing, GetCoursesPricingResponse>(request ?? new GetCoursesPricingRequest());

        [HttpGet]
        [Route("{courseId}/prices")]
        public Task<IActionResult> GetCoursesPricingByCourse(int courseId) =>
            ExecuteSp<CoursePricing, GetCoursesPricingByCourseResponse>(new GetCoursesPricingByCourseRequest
                {CourseId = courseId});

        [HttpPut]
        [Route("prices")]
        public Task<IActionResult> UpsertCoursePricing(UpsertCoursePricingRequest request) =>
            ExecuteSp<CoursePricing, UpsertCoursePricingResponse>(request);

        [HttpPut]
        [Route("")]
        public Task<IActionResult> UpsertCourse(InsertNewCourseRequest request) =>
            ExecuteSp<Course, InsertNewCourseResponse>(request);

        [HttpDelete]
        [Route("{courseId}")]
        public Task<IActionResult> DeleteCourse(int courseId) =>
            ExecuteSp<Course, DeleteCourseResponse>(new DeleteCourseRequest {CourseId = courseId});
    }
}