using Casgem.EntityLayer.Concrete;
using DataAccessLayer.Abstract;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casgem.BusinessLayer.MongoConcrete
{
    public class EstateMongoManager : IMongoEstateService
    {
        private readonly IMongoEstateDal _mongoEstateDal;

        public EstateMongoManager(IMongoEstateDal mongoEstateDal)
        {
            _mongoEstateDal = mongoEstateDal;
        }

        public Estate Create(Estate estate)
        {
            return _mongoEstateDal.Create(estate);
        }

        public List<Estate> Get()
        {
            return _mongoEstateDal.Get();
        }

        public Estate Get(string id)
        {
            return _mongoEstateDal.Get(id);
        }

        public List<Estate> GetByFilter(string? city, string? type, int? room, string? title, int? price, string? buildYear)
        {
            var filterBuilder = Builders<Estate>.Filter;
            var filter = filterBuilder.Empty;
            if (!string.IsNullOrEmpty(city))
            {
                filter = filter & filterBuilder.Where(estate => estate.City.ToLower().Contains(city.ToLower()));
            }

            if (!string.IsNullOrEmpty(type))
            {
                filter = filter & filterBuilder.Where(estate => estate.Type.ToLower().Contains(type.ToLower()));
            }

            if (room.HasValue)
            {
                filter = filter & filterBuilder.Eq(estate => estate.Room, room.Value);
            }

            if (!string.IsNullOrEmpty(title))
            {
                filter = filter & filterBuilder.Where(estate => estate.Title.ToLower().Contains(title.ToLower()));
            }

            if (price.HasValue)
            {
                filter = filter & filterBuilder.Eq(estate => estate.Price, price.Value);
            }

            if (!string.IsNullOrEmpty(buildYear))
            {
                filter = filter & filterBuilder.Eq(estate => estate.BuildYear, buildYear);
            }
            return _mongoEstateDal.GetByFilter(filter);
        }

        public void Remove(string id)
        {
            _mongoEstateDal.Remove(id);
        }

        public void Update(string id, Estate estate)
        {
            _mongoEstateDal.Update(id, estate);
        }
    }
}