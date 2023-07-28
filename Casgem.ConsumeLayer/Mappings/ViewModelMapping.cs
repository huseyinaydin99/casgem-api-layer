using AutoMapper;
using Casgem.ConsumeLayer.Models;
using Casgem.EntityLayer.Concrete;

namespace Casgem.ConsumeLayer.Mappings
{
    public class ViewModelMapping : Profile
    {
        public ViewModelMapping()
        {
            CreateMap<AddEstateModel, Estate>().ReverseMap();
        }
    }
}
