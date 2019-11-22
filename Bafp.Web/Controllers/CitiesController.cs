using System.Threading.Tasks;
using AutoMapper;
using Bafp.Contracts.Database;
using Bafp.Logic.Models;
using Bafp.Logic.Services;
using Bafp.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bafp.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public CitiesController(IMapper mapper, IDatabaseService databaseService)
        {
            _mapper = mapper;
            _databaseService = databaseService;
        }

        [HttpGet]
        [Route("")]
        public Task<GetAllCitiesResponse> GetAll() => ExecuteSp<CityDto, GetAllCitiesResponse>(new GetAllCitiesRequest());

        [HttpPut]
        [Route("")]
        public Task<InsertNewCityResponse> Insert(InsertNewCityRequest request) => ExecuteSp<Null, InsertNewCityResponse>(request);

        private async Task<TOut> ExecuteSp<TModel, TOut>(object request)
        {
            var result = await _databaseService.ExecuteStoredProcedure<TModel>(request);
            return _mapper.Map<TOut>(result);
        }
    }
}
