using Bafp.Contracts.Models;

namespace Bafp.Web.Models
{
    public class UpsertCityCourseRequest : HttpRequest
    {
        public CityCourseDto CityCourse { get; set; }
    }

    public class UpsertCityCourseResponse : HttpResponse
    {
        public CityCourseDto CityCourse { get; set; }
    }
}
