using Bafp.Logic.Models;

namespace Bafp.Web.Models
{
    public class InsertNewCourseRequest : HttpRequest
    {
        public CourseDto Course { get; set; }
    }

    public class InsertNewCourseResponse : HttpResponse
    {
        public CourseDto Course { get; set; }
    }
}
