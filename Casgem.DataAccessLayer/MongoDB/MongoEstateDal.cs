using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DataAccessLayer.Concrete
{
    public class MongoEstateDal : IMongoEstateDal
    {
        private readonly IMongoCollection<Estate> _estate;

        public MongoEstateDal(IEstateOfficeDatabaseSetting settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _estate = database.GetCollection<Estate>(settings.EstateCollectionName);
        }
        public Estate Create(Estate estate)
        {
            estate.Id = ObjectId.GenerateNewId().ToString();
            _estate.InsertOne(estate);
            return estate;
        }

        public List<Estate> Get()
        {
            return _estate.Find(estate => true).ToList();
        }

        public Estate Get(string id)
        {
            return _estate.Find(estate => estate.Id == id).FirstOrDefault();
        }

        public List<Estate> GetByFilter(FilterDefinition<Estate> filter)
        {

            return _estate.Find(filter).ToList();
        }

        public void Remove(string id)
        {
            _estate.DeleteOne(estate => estate.Id == id);
        }

        public void Update(string id, Estate estate)
        {
            _estate.ReplaceOne(estate => estate.Id == id, estate);
        }
    }
}
