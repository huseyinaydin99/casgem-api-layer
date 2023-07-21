using Casgem.BusinessLayer.Abstract;
using Casgem.EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Casgem.ApiLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public IActionResult CustomerList()
        {
            var user = HttpContext.Session.GetString("username");
            if (user != null && user != "")
            {
                return Ok(_customerService.TGetList());
            }
            return Unauthorized();
        }

        [HttpPost]
        public IActionResult AddCustomer(Customer customer)
        {
            var user = HttpContext.Session.GetString("username");
            if (user != null && user != "")
            {
                _customerService.TInsert(customer);
                return Ok();
            }
            return Unauthorized();
        }

        [HttpGet("{id}")]
        public IActionResult GetCustomer(int id)
        {
            var user = HttpContext.Session.GetString("username");
            if (user != null && user != "")
            {
                var value = _customerService.TGetById(id);
                return Ok(value);
            }
            return Unauthorized();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            var user = HttpContext.Session.GetString("username");
            if (user != null && user != "")
            {
                var value = _customerService.TGetById(id);
                _customerService.TDelete(value);
                return Ok();
            }
            return Unauthorized();
        }

        [HttpPut]
        public IActionResult UpdateCustomer(Customer customer)
        {
            var user = HttpContext.Session.GetString("username");
            if (user != null && user != "")
            {
                _customerService.TUpdate(customer);
                return Ok();
            }
            return Unauthorized();
        }

    }
}