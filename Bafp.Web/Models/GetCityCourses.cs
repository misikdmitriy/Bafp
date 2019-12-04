using Bafp.Web.Dto;

namespace Bafp.Web.Models
{
    public class GetCityCoursesRequest : HttpRequest
    {
    }

    public class GetCityCoursesResponse : HttpResponse
    {
        public CityCourseDto[] CityCourses { get; set; }
    }
}