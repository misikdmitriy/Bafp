using Bafp.Logic.Models;

namespace Bafp.Web.Models
{
    public class GetAllCityCoursesRequest : HttpRequest
    {
        public int CityId { get; set; }
    }

    public class GetAllCityCoursesResponse : HttpResponse
    {
        public CityCourseDto[] CityCourses { get; set; }
    }
}
