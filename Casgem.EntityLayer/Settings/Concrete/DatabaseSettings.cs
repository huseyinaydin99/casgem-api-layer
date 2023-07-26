using EntityLayer.Settings.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Settings.Concrete
{
    public class DatabaseSettings:IDatabaseSettings
    {
        public string CustomerCollectionName { get; set; }
        public string ProductCollectionName { get; set; }
        public string CategoryCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }






        /*

        public string CityCollectionName { get; set; }
        public string TestimonialCollectionName { get; set; }
        public string VideoCollectionName { get; set; }
        public string WhyusCollectionName { get; set; }
        public string TeamCollectionName { get; set; }
        public string ServiceBannerCollectionName { get; set; }
        public string ServiceWedoCollectionName { get; set; }
        public string PropertyCollectionName { get; set; }
        public string ContactCollectionName { get; set; }
        public string MessageCollectionName { get; set; }
        public string SubscribeCollectionName { get; set; }
        

        */
    }
}
