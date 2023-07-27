using Casgem.EntityLayer.Concrete;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IMongoEstateDal
    {
        List<Estate> Get();
        Estate Get(string id);
        List<Estate> GetByFilter(FilterDefinition<Estate> filter);
        Estate Create(Estate estate);
        void Update(string id, Estate estate);
        void Remove(string id);
    }
}