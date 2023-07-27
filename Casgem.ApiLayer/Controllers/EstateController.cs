using Casgem.BusinessLayer.MongoAbstract;
using Casgem.EntityLayer.Concrete;
using DataAccessLayer.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace Casgem.ApiLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstateController : ControllerBase
    {
        private readonly IMongoEstateService _mongoEstateService;

        public EstateController(IMongoEstateService mongoEstateService)
        {
            _mongoEstateService = mongoEstateService;
        }

        [HttpGet]
        public ActionResult<List<Estate>> Get()
        {
            return _mongoEstateService.Get();
        }

        [HttpGet("{id}")]
        public ActionResult<Estate> Get(string id)
        {
            var essate = _mongoEstateService.Get(id);
            if (essate == null)
            {
                return NotFound($"Essate with Id = {id} not found");
            }

            return essate;
        }

        [HttpPost]
        public ActionResult<Estate> Post([FromBody] Estate estate)
        {
            estate.Id = ObjectId.GenerateNewId().ToString();
            _mongoEstateService.Create(estate);

            return CreatedAtAction(nameof(Get), new { id = estate.Id }, estate);
        }


        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] Estate estate)
        {
            var existingEssate = _mongoEstateService.Get(id);
            if (existingEssate == null)
            {
                return NotFound($"Essate with Id = {id} not found");
            }

            _mongoEstateService.Update(id, estate);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var essate = _mongoEstateService.Get(id);
            if (essate == null)
            {
                return NotFound($"Essate with Id = {id} not found");
            }

            _mongoEstateService.Remove(essate.Id);
            return Ok($"Essate with id = {id} deleted");
        }


        //[HttpGet("filter")]
        //public ActionResult<List<Estate>> GetByFilter([FromQuery] string? city = null, [FromQuery] string? type = null, 
        //    [FromQuery] int? room = null, [FromQuery] string? title = null, [FromQuery] int? price = null, [FromQuery] string? buildYear = null) 
        //{
        //    var estate = _estateService.GetByFilter(city, type, room, title, price, buildYear);

        //    if(estate.Count == 0)
        //    {
        //        return NotFound("No estate found for the specified filter");
        //    }
        //    return Ok(estate);
        //}


        [HttpGet("filter")]
        public ActionResult<List<Estate>> GetByFilter([FromQuery] string? city = null, [FromQuery] string? type = null,
            [FromQuery] int? room = null, [FromQuery] string? title = null, [FromQuery] int? price = null, [FromQuery] string? buildYear = null)
        {
            var estate = _mongoEstateService.GetByFilter(city, type, room, title, price, buildYear);

            if (estate.Count == 0)
            {
                return NotFound("No estate found for the specified filter");
            }
            return Ok(estate);
        }
    }
}