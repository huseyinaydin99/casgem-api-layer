using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Casgem.ConsumeLayer.Models
{
    public class UpdateEstateModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public int Price { get; set; }
        public string City { get; set; }
        public int Room { get; set; }
        public string BuildYear { get; set; }
        public string Type { get; set; }
    }
}
