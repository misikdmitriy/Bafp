using Bafp.Web.Dto;

namespace Bafp.Web.Models
{
    public class UpsertCoursePricingRequest : HttpRequest
    {
        public CoursePricingDto CoursePricing { get; set; }
    }

    public class UpsertCoursePricingResponse : HttpResponse
    {
        public CoursePricingDto CoursePricing { get; set; }
    }
}
