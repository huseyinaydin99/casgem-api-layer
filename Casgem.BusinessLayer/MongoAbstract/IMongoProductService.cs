using Casgem.DtoLayer.DTOs.ProductDTOs;
using Casgem.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casgem.BusinessLayer.MongoAbstract
{
    public interface IMongoProductService
    {
        void Insert(CreateProductDto t);
        void Update(UpdateProductDto t);
        void Delete(DeleteProductDto t);
        Task<List<GetProductDto>> GetList();
        Task<GetProductDto> GetById(int id);
    }
}
