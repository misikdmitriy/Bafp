using Bafp.Web.Dto;

namespace Bafp.Web.Models
{
    public class DeleteCourseRequest : HttpRequest
    {
        public int CourseId { get; set; }
    }

    public class DeleteCourseResponse : HttpResponse
    {
        public CourseDto Course { get; set; }
    }
}
