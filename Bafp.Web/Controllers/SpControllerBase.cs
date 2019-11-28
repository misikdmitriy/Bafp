using System.Threading.Tasks;
using AutoMapper;
using Bafp.Logic.Services;
using Bafp.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bafp.Web.Controllers
{
    public class SpControllerBase : ControllerBase
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        protected SpControllerBase(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }

        protected async Task<IActionResult> ExecuteSp<TModel, TOut>(object request)
            where TOut : HttpResponse
        {
            var result = await _databaseService.ExecuteStoredProcedure<TModel>(request);
            var response = _mapper.Map<TOut>(result);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
