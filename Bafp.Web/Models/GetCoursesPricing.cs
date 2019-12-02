using Bafp.Contracts.Models;

namespace Bafp.Web.Models
{
    public class GetCoursesPricingRequest : HttpRequest
    {
        public int CategoryId { get; set; }
    }

    public class GetCoursesPricingResponse : HttpResponse
    {
        public CoursePricingDto[] CoursePriceList { get; set; }
    }
}
