using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MVC_Watch_Business.DTO.AppUserDTO;
using MVC_Watch_Business.DTO.BrandDTO;
using MVC_Watch_Business.DTO.CartItemDTO;
using MVC_Watch_Business.DTO.CategoryDTO;
using MVC_Watch_Business.DTO.ProductDiscountDTO;
using MVC_Watch_Business.DTO.ProductDTO;
using MVC_Watch_Business.DTO.StockDTO;
using MVC_Watch_Data.Models;
namespace MVC_Watch_UI.Mappings
{
	public class AutoMapperProfiles : Profile
	{
        public AutoMapperProfiles()
        {
			CreateMap<Category, CategoryDTO>().ReverseMap();
			CreateMap<Category, AddCateDTO>().ReverseMap();
			CreateMap<Category, UpdateCateDTO>().ReverseMap();
			CreateMap<UpdateCateDTO, CategoryDTO>().ReverseMap();
			CreateMap<Brand, BrandDTO>().ReverseMap();
			CreateMap<Brand,UpdateBrandDTO>().ReverseMap();
			CreateMap<Brand,AddBrandDTO>().ReverseMap();
			CreateMap<BrandDTO,UpdateBrandDTO>().ReverseMap();
			CreateMap<Product, ProductDTO>().ReverseMap();
			CreateMap<Product, AddProductDTO>().ReverseMap();
			CreateMap<Product, UpdateProductDTO>().ReverseMap();
			CreateMap<ProductDTO, UpdateProductDTO>().ReverseMap();
			CreateMap<CartItem,CartItemDTO>().ReverseMap();
			CreateMap<CartItem, AddCartItemDTO>().ReverseMap();
			CreateMap<CartItem, UpdateCartItemDTO>().ReverseMap();
			CreateMap<ProductDiscount,ProductDiscountDTO>().ReverseMap();
			CreateMap<ProductDiscount,AddProductDiscountDTO>().ReverseMap();
			CreateMap<ProductDiscount,UpdateProductDiscountDTO>().ReverseMap();
			CreateMap<ProductDiscountDTO,UpdateProductDiscountDTO>().ReverseMap();
			CreateMap<Stock,StockDTO>().ReverseMap();
			CreateMap<Stock,AddStockDTO>().ReverseMap();
			CreateMap<Stock,UpdateStockDTO>().ReverseMap();
			CreateMap<StockDTO, UpdateStockDTO>().ReverseMap();
			CreateMap<AppUserDTO, AppUser>().ReverseMap();

		}
	}
}
