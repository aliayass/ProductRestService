using AutoMapper;
using SOAPProduct.Dto;
using SOAPProduct.Models;

namespace SOAPProduct.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
        }
    }
}
