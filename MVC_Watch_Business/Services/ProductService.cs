using AutoMapper;
using MVC_Watch_Business.DTO.ProductDTO;
using MVC_Watch_Data.Contracts;
using MVC_Watch_Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Watch_Business.Services
{
	public class ProductService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<IEnumerable<ProductDTO>> GetAllProductsAsync(Expression<Func<Product, bool>>? filter = null, string? includeProperties = null)
		{
			try
			{
				var productsDomain = await _unitOfWork.Product.GetAllAsync(filter,includeProperties);
				var products = _mapper.Map<IEnumerable<ProductDTO>>(productsDomain);
				return products;
			}
			catch (Exception)
			{

				throw;
			}

		}
		public async Task<ProductDTO> GetProductAsync(Expression<Func<Product, bool>> filter, string? includeProperties = null)
		{
			try
			{ 
				var productDomain =  await _unitOfWork.Product.GetFirstOrDefaultAsync(filter, includeProperties);
				var product = _mapper.Map<ProductDTO>(productDomain);
				return product;
			}
			catch (Exception)
			{

				throw;
			}
		}
		public void AddProduct(AddProductDTO product)
		{
			try
			{
				var productDomain = _mapper.Map<Product>(product);
				_unitOfWork.Product.Add(productDomain);
			}
			catch (Exception)
			{

				throw;
			}
		}
		public void UpdateProduct(UpdateProductDTO product)
		{
			try
			{
				var productDomain = _mapper.Map<Product>(product);
				_unitOfWork.Product.Update(productDomain);
			}
			catch (Exception)
			{

				throw;
			}
		}
		public void DeleteProduct(ProductDTO product)
		{
			try
			{
				var productDomain = _mapper.Map<Product>(product);
				_unitOfWork.Product.Delete(productDomain);
			}
			catch (Exception)
			{

				throw;
			}
		}
		public async Task<string> GetImageUrlOfProduct(Expression<Func<Product, bool>> filter)
		{
			var productDomain = await GetProductAsync(filter);
			var imgPath = productDomain.ImageUrl;
			return imgPath ?? String.Empty;
		}
		public async Task<double> GetCurrentPriceOfProduct(Guid productID)
		{
			var product = await _unitOfWork.Product.GetFirstOrDefaultAsync(u => u.ProductID == productID, includeProperties:"ProductDiscount");
			if (product.ProductDiscount != null)
			{
				return (double)product.Price * (1 - (product.ProductDiscount.Percentage)/100);
			} else { 
				return (double)product.Price; 
			}

		}
	}
}
