using Bafp.Web.Dto;

namespace Bafp.Web.Models
{
    public class GetCoursesPricingByCourseRequest : HttpRequest
    {
        public int CourseId { get; set; }
    }

    public class GetCoursesPricingByCourseResponse : HttpResponse
    {
        public CoursePricingDto[] CoursePriceList { get; set; }
    }
}
