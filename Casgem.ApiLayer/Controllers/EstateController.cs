using Casgem.BusinessLayer.MongoAbstract;
using Casgem.EntityLayer.Concrete;
using DataAccessLayer.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace Casgem.ApiLayer.Controllers
{
    [Route("api/[controller]/")]
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

            return Ok(essate);
        }

        [HttpPost]
        public ActionResult<Estate> Post(Estate estate)
        {
            estate.Id = ObjectId.GenerateNewId().ToString();
            _mongoEstateService.Create(estate);

            return Ok();
        }


        [HttpPut("{id}")]
        public ActionResult<Estate> Put(string id, Estate estate)
        {
            var existingEssate = _mongoEstateService.Get(id);
            if (existingEssate == null)
            {
                return NotFound($"Essate with Id = {id} not found");
            }

            _mongoEstateService.Update(id, estate);
            return Ok(estate);
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
        public ActionResult<List<Estate>> GetByFilter( string? city = null,  string? type = null,
            int? room = null, string? title = null, int? price = null, string? buildYear = null)
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