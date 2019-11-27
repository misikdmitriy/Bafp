namespace Bafp.Web.Models
{
    public class UpsertCityCourseRequest : HttpRequest
    {
        public int CityId { get; set; }
        public int CourseId { get; set; }
        public int Count { get; set; }
    }

    public class UpsertCityCourseResponse : HttpResponse
    {
    }
}
