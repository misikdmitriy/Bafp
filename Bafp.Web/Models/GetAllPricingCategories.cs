using Bafp.Logic.Models;

namespace Bafp.Web.Models
{
    public class GetAllPricingCategoriesRequest : HttpRequest
    {
    }

    public class GetAllPricingCategoriesResponse : HttpResponse
    {
        public PricingCategoryDto[] Categories { get; set; }
    }
}
