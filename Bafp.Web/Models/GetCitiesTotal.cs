using Bafp.Contracts.Models;

namespace Bafp.Web.Models
{
    public class GetCitiesTotalRequest : HttpRequest
    {
    }

    public class GetCitiesTotalResponse : HttpResponse
    {
        public CityTotalDto[] Total { get; set; }
    }
}