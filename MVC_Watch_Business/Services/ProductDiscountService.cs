using AutoMapper;
using MVC_Watch_Business.DTO.ProductDiscountDTO;
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
	public class ProductDiscountService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		public ProductDiscountService(IUnitOfWork unitOfWork, IMapper mapper) {
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}
		public async Task<IEnumerable<ProductDiscountDTO>> GetAllProductDiscountsAsync(Expression<Func<ProductDiscount, bool>>? filter = null, string? includeProperties = null)
		{
			try
			{
				var productDiscountsDomain = await _unitOfWork.ProductDiscount.GetAllAsync(filter, includeProperties);
				var productDiscounts = _mapper.Map<IEnumerable<ProductDiscountDTO>>(productDiscountsDomain);
				return productDiscounts;

			}
			catch (Exception)
			{

				throw;
			}
		}
		public async Task<ProductDiscountDTO> GetProductDiscountAsync(Expression<Func<ProductDiscount, bool>> filter, string? includeProperties = null)
		{
			try
			{
				var productDisocuntDomain = await _unitOfWork.ProductDiscount.GetFirstOrDefaultAsync(filter, includeProperties);
				var productDiscount = _mapper.Map<ProductDiscountDTO>(productDisocuntDomain);
				return productDiscount;

			}
			catch (Exception)
			{

				throw;
			}
		}
		public void AddProductDiscount(AddProductDiscountDTO productDiscount)
		{
			try
			{
				var productDiscountDomain = _mapper.Map<ProductDiscount>(productDiscount);
				_unitOfWork.ProductDiscount.Add(productDiscountDomain);
                _unitOfWork.Save();
            }
			catch (Exception)
			{

				throw;
			}
		}
		public void UpdateProductDiscount(UpdateProductDiscountDTO productDiscount)
		{
			try
			{
				var productDiscountDomain = _mapper.Map<ProductDiscount>(productDiscount);
				_unitOfWork.ProductDiscount.Update(productDiscountDomain);
                _unitOfWork.Save();
            }
			catch (Exception)
			{

				throw;
			}
		}
		public void DeleteProductDiscount(ProductDiscountDTO productDiscount)
		{
			try
			{
				var productDiscountDomain = _mapper.Map<ProductDiscount>(productDiscount);
				_unitOfWork.ProductDiscount.Delete(productDiscountDomain);
                _unitOfWork.Save();
            }
			catch (Exception)
			{

				throw;
			}
		}

	}
}
