using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Bafp.Logic.Services;
using Bafp.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bafp.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly ISmartExecutor _smartExecutor;
        private readonly IMapper _mapper;

        public CitiesController(IMapper mapper, ISmartExecutor smartExecutor)
        {
            _mapper = mapper;
            _smartExecutor = smartExecutor;
        }

        // GET api/values
        [HttpGet]
        [Route("")]
        public Task<GetAllCitiesResponse> GetAll() => ExecuteSp<CityDto, GetAllCitiesResponse>(new GetAllCitiesRequest());

        private async Task<TOut> ExecuteSp<TModel, TOut>(object request)
        {
            var result = await _smartExecutor.ExecuteSp<TModel>(request);
            return _mapper.Map<TOut>(result);
        }
    }
}
