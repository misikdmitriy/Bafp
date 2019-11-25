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
    public class CoursesController : SpControllerBase
    {
        public CoursesController(IDatabaseService databaseService, IMapper mapper) : base(databaseService, mapper)
        {
        }

        [HttpGet]
        [Route("")]
        public Task<GetAllCoursesResponse> GetAll() => ExecuteSp<CourseDto, GetAllCoursesResponse>(new GetAllCoursesRequest());

        [HttpPut]
        [Route("")]
        public Task<InsertNewCourseResponse> Insert(InsertNewCourseRequest request) => ExecuteSp<Null, InsertNewCourseResponse>(request);
    }
}