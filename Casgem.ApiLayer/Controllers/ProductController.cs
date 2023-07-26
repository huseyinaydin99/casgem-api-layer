using AutoMapper;
using Casgem.BusinessLayer.Abstract;
using Casgem.BusinessLayer.MongoAbstract;
using Casgem.DtoLayer.DTOs.ProductDTOs;
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
        private readonly IMongoProductService _mongoProductService;
        private readonly IMapper _mapper;

        public ProductController(IProductService productService, IMongoProductService mongoProductService, IMapper mapper)
        {
            _productService = productService;
            _mongoProductService = mongoProductService;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult ProductList()
        {
            var user = HttpContext.Session.GetString("username");
            if (user != null && user != "")
            {
                /*var values = _productService.TGetList();
                return Ok(values);*/
                var values = _mongoProductService.GetList();
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
                /*var value = _productService.TGetById(id);
                return Ok(value);*/
                var values = _mongoProductService.GetById(id);
                return Ok(values);
            }
            return Unauthorized();
        }

        [HttpPost]
        public ActionResult ProductAdd(Product product)
        {
            var user = HttpContext.Session.GetString("username");
            if (user != null && user != "")
            {
                var value = _mapper.Map<CreateProductDto>(product);
                _mongoProductService.Insert(value);
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
                var value = _mongoProductService.GetById(id);
                var dto = _mapper.Map<DeleteProductDto>(value);
                _mongoProductService.Delete(dto);
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
                _mongoProductService.Update(_mapper.Map<UpdateProductDto>(product));
                return Ok();
            }
            return Unauthorized();
        }
        /*
        [Route("ProductListWithCategory")]
        [HttpGet]
        public IActionResult ProductListWithCategories()
        {
            var user = HttpContext.Session.GetString("username");
            if (user != null && user != "")
            {
                var values = _mongoProductService.GetProductWithCategories();
                return Ok(values);
            }
            return Unauthorized();
        }
        */
    }
}
