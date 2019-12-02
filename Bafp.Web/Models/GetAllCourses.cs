using Bafp.Contracts.Models;

namespace Bafp.Web.Models
{
    public class GetCoursesRequest : HttpRequest
    {
        public int[] Ids { get; set; } = new int[0];
    }

    public class GetCoursesResponse : HttpResponse
    {
        public CourseDto[] Courses { get; set; }
    }
}
