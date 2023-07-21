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
            var user = HttpContext.Session.GetString("username");
            if(user != null && user != "")
            {
                var values = _productService.TGetList();
                return Ok(values);
            }
            return Unauthorized();
        }

        [HttpGet("{id}")]
        public ActionResult GetProduct(int id)
        {
            var user = HttpContext.Session.GetString("username");
            if (user != null && user != "")
            {
                var value = _productService.TGetById(id);
                return Ok(value);
            }
            return Unauthorized();
        }

        [HttpPost]
        public ActionResult ProductAdd(Product product)
        {
            var user = HttpContext.Session.GetString("username");
            if (user != null && user != "")
            {
                _productService.TInsert(product);
                return Ok();
            }
            return Unauthorized();
        }

        [HttpDelete("{id}")]
        public ActionResult ProductDelete(int id)
        {
            var user = HttpContext.Session.GetString("username");
            if (user != null && user != "")
            {
                var value = _productService.TGetById(id);
                _productService.TDelete(value);
                return Ok();
            }
            return Unauthorized();
        }

        [HttpPut]
        public ActionResult UpdateProduct(Product product)
        {
            var user = HttpContext.Session.GetString("username");
            if (user != null && user != "")
            {
                _productService.TUpdate(product);
                return Ok();
            }
            return Unauthorized();
        }

        [Route("ProductListWithCategory")]
        [HttpGet]
        public IActionResult ProductListWithCategories()
        {
            var user = HttpContext.Session.GetString("username");
            if (user != null && user != "")
            {
                var values = _productService.GetProductWithCategories();
                return Ok(values);
            }
            return Unauthorized();
        }
    }
}
