using Casgem.DtoLayer.DTOs.CustomerDTOs;

namespace Casgem.BusinessLayer.MongoAbstract
{
    public interface IMongoCustomerService
    {
        void Insert(CreateCustomerDto t);
        void Update(UpdateCustomerDto t);
        void Delete(DeleteCustomerDto t);
        Task<List<GetCustomerDto>> GetList();
        Task<GetCustomerDto> GetById(int id);
    }
}