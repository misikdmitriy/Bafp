using Bafp.Logic.Models;

namespace Bafp.Web.Models
{
    public class GetCoursesPricingRequest : HttpRequest
    {
        public string CategoryName { get; set; }
    }

    public class GetCoursesPricingResponse : HttpResponse
    {
        public CoursePricingDto[] CoursePriceList { get; set; }
    }
}
