namespace Bafp.Web.Models
{
    public class AddCityCourseRequest : HttpRequest
    {
        public string CityName { get; set; }
        public string CourseName { get; set; }
        public int Count { get; set; }
    }

    public class AddCityCourseResponse : HttpResponse
    {
    }
}
