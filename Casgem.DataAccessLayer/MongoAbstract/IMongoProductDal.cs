using Casgem.DtoLayer.DTOs.ProductDTOs;
using Casgem.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casgem.DataAccessLayer.MongoAbstract
{
    public interface IMongoProductDal
    {
        void Insert(CreateProductDto t);
        void Update(UpdateProductDto t);
        void Delete(DeleteProductDto t);
        Task<List<GetProductDto>> GetList();
        Task<GetProductDto> GetById(int id);
    }
}
