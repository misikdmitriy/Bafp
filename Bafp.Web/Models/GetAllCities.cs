using Bafp.Logic.Models;

namespace Bafp.Web.Models
{
    public class GetAllCitiesRequest : HttpRequest
    {
    }

    public class GetAllCitiesResponse : HttpResponse
    {
        public CityDto[] Cities { get; set; }
    }
}
