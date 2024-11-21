using AutoMapper;
using BikeStoreApp.DTOs;
using BikeStoreProject.Dto;
using BikeStoreProject.Models;
using Student_13WebApiProject.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BikeStoreProject.AutoMapper
{
    public class Mapper : Profile
    {

        public Mapper()
        {
            CreateMap<CreateCustomerDto, Customer>().ReverseMap();
            CreateMap<Customer, ResponseCustomerDto>().ReverseMap();
            CreateMap<UpdateCustomerDto, Customer>().ReverseMap();

            CreateMap<CreateOrderDto, Order>().ReverseMap();
            CreateMap<Order, ResponseOrderDto>().ReverseMap();

            CreateMap<CreateOrderItemDto, OrderItem>().ReverseMap();
            CreateMap<OrderItem, ResponseOrderItemDto>().ReverseMap();

            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryCreateDto, Category>();

            CreateMap<Brand, BrandDto>();
            CreateMap<BrandCreateDto, Brand>();

            CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductName))
            .ForMember(dest => dest.BrandId, opt => opt.MapFrom(src => src.BrandId))
            .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
            .ForMember(dest => dest.ModelYear, opt => opt.MapFrom(src => src.ModelYear))
            .ForMember(dest => dest.ListPrice, opt => opt.MapFrom(src => src.ListPrice));
            CreateMap<ProductCreateUpdateDto, Product>();

        }
    }
}
