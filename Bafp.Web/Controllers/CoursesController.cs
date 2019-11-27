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
        public Task<IActionResult> GetAll() => ExecuteSp<CourseDto, GetAllCoursesResponse>(new GetAllCoursesRequest());

        [HttpGet]
        [Route("prices/{categoryName}")]
        public Task<IActionResult> GetCoursesPricing(string categoryName) => ExecuteSp<CoursePricingDto, GetCoursesPricingResponse>(new GetCoursesPricingRequest { CategoryName = categoryName });

        [HttpPut]
        [Route("")]
        public Task<IActionResult> Insert(InsertNewCourseRequest request) => ExecuteSp<Null, InsertNewCourseResponse>(request);
    }
}