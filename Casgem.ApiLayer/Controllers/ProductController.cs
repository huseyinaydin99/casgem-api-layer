using Casgem.BusinessLayer.Abstract;
using Casgem.EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Casgem.ApiLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public ActionResult ProductList() 
        {
            var values = _productService.TGetList();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public ActionResult GetProduct(int id)
        {
            var value = _productService.TGetById(id);
            return Ok(value);
        }

        [HttpPost]
        public ActionResult ProductAdd(Product product)
        {
            _productService.TInsert(product);
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult ProductDelete(int id)
        {
            var value = _productService.TGetById(id);
            _productService.TDelete(value);
            return Ok();
        }

        [HttpPut]
        public ActionResult UpdateProduct(Product product)
        {
            _productService.TUpdate(product);
            return Ok();
        }

        [Route("ProductListWithCategory")]
        [HttpGet]
        public IActionResult ProductListWithCategories()
        {
            var values = _productService.GetProductWithCategories();
            return Ok(values);
        }
    }
}
