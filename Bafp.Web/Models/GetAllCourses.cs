using Bafp.Logic.Models;

namespace Bafp.Web.Models
{
    public class GetAllCoursesRequest : HttpRequest
    {
    }

    public class GetAllCoursesResponse : HttpResponse
    {
        public CourseDto[] Courses { get; set; }
    }
}
