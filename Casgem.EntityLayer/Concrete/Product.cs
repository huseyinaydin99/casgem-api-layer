using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casgem.EntityLayer.Concrete
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Brand { get; set; }
        public int Stock { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
