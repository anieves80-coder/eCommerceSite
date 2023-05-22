using EStore.web.Models.Domain;
using EStore.web.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EStore.web.Controllers
{
    [ApiController]
    [Route("api/[controller]/{criteria}")]
    public class SearchController : Controller
    {

        private IProductsRepository productRepsitory;

        private List<ProductsModel> searchResults { get; set; }
        public SearchController(IProductsRepository productRepsitory)
        {
            this.productRepsitory = productRepsitory;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string criteria)
        {
            searchResults = (await productRepsitory.FindByCatAsync(criteria)).ToList();
            return Ok(searchResults);
        }
    }
}
