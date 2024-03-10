using AutoMapper;
using MVC_Watch_Business.DTO.BrandDTO;
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
	public class BrandService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		public BrandService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<IEnumerable<BrandDTO>> GetAllBrandsAsync(Expression<Func<Brand, bool>>? filter = null)
		{
			try
			{
				var brandsDomain = await _unitOfWork.Brand.GetAllAsync(filter);
				var brands = _mapper.Map<IEnumerable<BrandDTO>>(brandsDomain);			
				return brands;
			}
			catch (Exception)
			{

				throw;
			}			
		}

		public async Task<BrandDTO> GetBrandAsync(Expression<Func<Brand, bool>> filter)
		{
			try
			{
				var brandDomain = await _unitOfWork.Brand.GetFirstOrDefaultAsync(filter);
				var brand = _mapper.Map<BrandDTO>(brandDomain);
				return brand;
			}
			catch (Exception)
			{
				throw;
			}
		}

		public async void AddBrand(AddBrandDTO brand)
		{
			try
			{
				var brandDomain = _mapper.Map<Brand>(brand);
				_unitOfWork.Brand.Add(brandDomain);
				_unitOfWork.Save();
			}
			catch (Exception)
			{

				throw;
			}
		}

		public void UpdateBrand(UpdateBrandDTO brand)
		{
			try
			{
				var brandDomain = _mapper.Map<Brand>(brand);
				_unitOfWork.Brand.Update(brandDomain);
				 _unitOfWork.Save();
			}
			catch (Exception)
			{

				throw;
			}
		}

		public void DeleteBrand(BrandDTO brand)
		{
			try
			{
				var brandDomain = _mapper.Map<Brand>(brand);
				_unitOfWork.Brand.Delete(brandDomain);
                 _unitOfWork.Save();
            }
            catch (Exception)
			{

				throw;
			}
		}
		public async Task<string> GetImageUrlOfBrand(Expression<Func<Brand, bool>> filter)
		{
			var brandDomain = await GetBrandAsync(filter);
			var imgPath = brandDomain.BrandImageUrl;
			return imgPath ?? String.Empty;
		}
	}
}
