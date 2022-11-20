using AutoMapper;
using OrderAPI.Models.DTOs;
using OrderAPI.Models.Entities;

namespace OrderAPI.OrderDbContext
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Order, CreateOrderRequest>().ReverseMap();
            CreateMap<ProductDetailDto, OrderDetails>().ReverseMap(); 
        }
    }
}
