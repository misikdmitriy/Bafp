using Bafp.Logic.Models;

namespace Bafp.Web.Models
{
    public class GetAllCityCoursesByCourseRequest : HttpRequest
    {
        public int CourseId { get; set; }
    }

    public class GetAllCityCoursesByCourseResponse : HttpResponse
    {
        public CityCourseDto[] CityCourses { get; set; }
    }
}
