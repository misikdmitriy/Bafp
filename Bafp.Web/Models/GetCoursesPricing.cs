using Bafp.Contracts.Models;

namespace Bafp.Web.Models
{
    public class GetCoursesPricingRequest : HttpRequest
    {
        public int[] Ids { get; set; } = new int[0];
    }

    public class GetCoursesPricingResponse : HttpResponse
    {
        public CoursePricingDto[] CoursePriceList { get; set; }
    }
}
