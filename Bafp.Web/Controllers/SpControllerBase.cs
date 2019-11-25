using System.Threading.Tasks;
using AutoMapper;
using Bafp.Logic.Services;

namespace Bafp.Web.Controllers
{
    public class SpControllerBase
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        protected SpControllerBase(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }

        protected async Task<TOut> ExecuteSp<TModel, TOut>(object request)
        {
            var result = await _databaseService.ExecuteStoredProcedure<TModel>(request);
            return _mapper.Map<TOut>(result);
        }
    }
}
