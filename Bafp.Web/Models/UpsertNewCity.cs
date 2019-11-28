using Bafp.Logic.Models;

namespace Bafp.Web.Models
{
    public class UpsertNewCityRequest : HttpRequest
    {
        public CityDto City { get; set; }
    }

    public class UpsertNewCityResponse : HttpResponse
    {
        public CityDto City { get; set; }
    }
}
