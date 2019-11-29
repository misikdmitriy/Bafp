using System.Threading.Tasks;
using AutoMapper;
using Bafp.Logic.Services;
using Bafp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Bafp.Web.Controllers
{
    public class SpControllerBase : ControllerBase
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        protected SpControllerBase(IDatabaseService databaseService, IMapper mapper, ILogger logger)
        {
            _databaseService = databaseService;
            _mapper = mapper;
            _logger = logger;
        }

        protected async Task<IActionResult> ExecuteSp<TModel, TOut>(object request)
            where TOut : HttpResponse
        {
            _logger.Information("Try to execute request {@request}", request);
            
            var result = await _databaseService.ExecuteStoredProcedure<TModel>(request);
            var response = _mapper.Map<TOut>(result);
            
            _logger.Information("Mapped to {@response}", response);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
