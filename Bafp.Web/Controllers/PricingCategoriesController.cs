using System.Threading.Tasks;
using AutoMapper;
using Bafp.Contracts;
using Bafp.Logic.Models;
using Bafp.Logic.Services;
using Bafp.Web.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Bafp.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors(Constants.PolicyNames.AllowUi)]
    public class PricingCategoriesController : SpControllerBase
    {
        public PricingCategoriesController(IDatabaseService databaseService, IMapper mapper) : base(databaseService, mapper)
        {
        }

        [HttpGet]
        [Route("")]
        public Task<IActionResult> GetAllPricingCategories() => ExecuteSp<PricingCategoryDto, GetAllPricingCategoriesResponse>(new GetAllPricingCategoriesRequest());
    }
}
