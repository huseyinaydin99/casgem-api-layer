using Casgem.BusinessLayer.Abstract;
using Casgem.DataAccessLayer.Abstract;
using Casgem.EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Casgem.ApiLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        //private ISession _session;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
            //_session = session;
        }

        // GET, POST, PUT, DELETE, PATCH...
        [HttpGet]
        public IActionResult CategoryList()
        {
            var user = HttpContext.Session.GetString("username");
            if (user != null && user != "")
            {
                var values = _categoryService.TGetList();
                return Ok(values);
            }
            return Unauthorized();
        }

        [HttpPost]
        public IActionResult AddCategory(Category category)
        {
            var user = HttpContext.Session.GetString("username");
            //var user = _session.GetString("username");
            if (user != null && user != "")
            {
                _categoryService.TInsert(category);
                return Ok();
            }
            return Unauthorized();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            var user = HttpContext.Session.GetString("username");
            if (user != null && user != "")
            {
                var value = _categoryService.TGetById(id);
                _categoryService.TDelete(value);
                return Ok();
            }
            return Unauthorized();
        }

        [HttpGet("{id}")]
        public IActionResult GetCategory(int id)
        {
            var user = HttpContext.Session.GetString("username");
            if (user != null && user != "")
            {
                var value = _categoryService.TGetById(id);
                return Ok(value);
            }
            return Unauthorized();
        }

        [HttpPut]
        public IActionResult UpdateCategory(Category category)
        {
            var user = HttpContext.Session.GetString("username");
            if (user != null && user != "")
            {
                _categoryService.TUpdate(category);
                return Ok(category);
            }
            return Unauthorized();
        }

        /*[HttpGet]
        public async Task<IActionResult> AddCategory(Category category)
        {
            _categoryService.TInsert(category);
            return Ok();
        }*/
    }
}
