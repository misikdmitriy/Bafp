using Bafp.Logic.Models;

namespace Bafp.Web.Models
{
    public class InsertNewCityRequest : HttpRequest
    {
        public CityDto City { get; set; }
    }

    public class InsertNewCityResponse : HttpResponse
    {
    }
}
