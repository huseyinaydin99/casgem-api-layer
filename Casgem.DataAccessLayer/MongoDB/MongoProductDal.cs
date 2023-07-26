using AutoMapper;
using Casgem.DataAccessLayer.MongoAbstract;
using Casgem.DtoLayer.DTOs.ProductDTOs;
using Casgem.EntityLayer.Concrete;
using EntityLayer.Settings.Abstract;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casgem.DataAccessLayer.MongoDB
{
    public class MongoProductDal : IMongoProductDal
    {
        private readonly IMongoCollection<Product> _productCollection;
        private readonly IMapper _mapper;

        public MongoProductDal(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            _mapper = mapper;
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _productCollection = database.GetCollection<Product>(databaseSettings.ProductCollectionName);
        }

        public async void Delete(DeleteProductDto t)
        {
            await _productCollection.DeleteOneAsync(x => x.ProductId == t.ProductId);
        }

        public async Task<GetProductDto> GetById(int id)
        {
            //var value = await _productCollection.Find<Product>(x => x.ProductId == id).FirstOrDefaultAsync();
            var dto = _mapper.Map<GetProductDto>(_productCollection.Find<Product>(x => x.ProductId == id).FirstOrDefaultAsync());
            return dto;
        }

        public async Task<List<GetProductDto>> GetList()
        {
            var values = await _productCollection.Find(value => true).ToListAsync();
            return _mapper.Map<List<GetProductDto>>(values);
        }

        /*
        public Task<List<CreateProductDto>> GetProductWithCategories()
        {
            throw new NotImplementedException();
        }
        */

        public async void Insert(CreateProductDto t)
        {
            var values = _mapper.Map<Product>(t);
            await _productCollection.InsertOneAsync(values);
        }

        public async void Update(UpdateProductDto t)
        {
            var values = _mapper.Map<Product>(t);
            var result = await _productCollection.FindOneAndReplaceAsync(x => x.ProductId == t.ProductId, values);
        }
    }
}