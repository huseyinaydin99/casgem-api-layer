using AutoMapper;
using Casgem.BusinessLayer.MongoAbstract;
using Casgem.DataAccessLayer.MongoAbstract;
using Casgem.DtoLayer.DTOs.ProductDTOs;

namespace Casgem.BusinessLayer.MongoConcrete
{
    public class ProductManager : IMongoProductService
    {
        private readonly IMongoProductDal _mongoProductDal;
        private readonly IMapper _mapper;

        public ProductManager(IMongoProductDal productDal, IMapper mapper)
        {
            _mongoProductDal = productDal;
            _mapper = mapper;
        }

        public void Delete(DeleteProductDto t)
        {
            _mongoProductDal.Delete(t);
        }

        public Task<GetProductDto> GetById(int id)
        {
            return _mongoProductDal.GetById(id);
        }

        public Task<List<GetProductDto>> GetList()
        {
            return _mongoProductDal.GetList();
        }

        public void Insert(CreateProductDto t)
        {
            _mongoProductDal.Insert(t);
        }

        public void Update(UpdateProductDto t)
        {
            _mongoProductDal.Update(t);
        }
    }
}