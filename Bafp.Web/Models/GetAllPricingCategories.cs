using Bafp.Web.Dto;

namespace Bafp.Web.Models
{
    public class GetPricingCategoriesRequest : HttpRequest
    {
        public int[] Ids { get; set; } = new int[0];
    }

    public class GetPricingCategoriesResponse : HttpResponse
    {
        public PricingCategoryDto[] Categories { get; set; }
    }
}
