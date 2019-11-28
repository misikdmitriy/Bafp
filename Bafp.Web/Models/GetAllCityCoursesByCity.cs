using Bafp.Logic.Models;

namespace Bafp.Web.Models
{
    public class GetAllCityCoursesByCityRequest : HttpRequest
    {
        public int CityId { get; set; }
    }

    public class GetAllCityCoursesByCityResponse : HttpResponse
    {
        public CityCourseDto[] CityCourses { get; set; }
    }
}
