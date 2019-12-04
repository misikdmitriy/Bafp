using Bafp.Web.Dto;

namespace Bafp.Web.Models
{
    public class GetCitiesRequest : HttpRequest
    {
        public int[] Ids { get; set; } = new int[0];
    }

    public class GetCitiesResponse : HttpResponse
    {
        public CityDto[] Cities { get; set; }
    }
}
