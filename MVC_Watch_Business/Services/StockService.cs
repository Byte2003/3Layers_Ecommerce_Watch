using AutoMapper;
using MVC_Watch_Business.DTO.StockDTO;
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
	public class StockService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
        public StockService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
			_mapper = mapper;
        }
		public async Task<IEnumerable<StockDTO>> GetAllStocksAsync(Expression<Func<Stock, bool>>? filter = null, string? includeProperties = null)
		{
			try
			{
				var stocksDomain = await _unitOfWork.Stock.GetAllAsync(filter, includeProperties);
				var stocks = _mapper.Map<IEnumerable<StockDTO>>(stocksDomain);
				return stocks;

			}
			catch (Exception)
			{

				throw;
			}
		}
		public async Task<StockDTO> GetStockAsync(Expression<Func<Stock, bool>> filter, string? includeProperties = null)
		{
			try
			{
				var stockDomain = await _unitOfWork.Stock.GetAllAsync(filter, includeProperties);
				var stock = _mapper.Map<StockDTO>(stockDomain);
				return stock;

			}
			catch (Exception)
			{

				throw;
			}
		}
		public void AddStock(AddStockDTO stock)
		{
			try
			{
				var stockDomain = _mapper.Map<Stock>(stock);
				_unitOfWork.Stock.Add(stockDomain);
                _unitOfWork.Save();
            }
			catch (Exception)
			{

				throw;
			}
		}
		public void UpdateStock(UpdateStockDTO stock)
		{
			try
			{
				var stockDomain = _mapper.Map<Stock>(stock);
				_unitOfWork.Stock.Update(stockDomain);
                _unitOfWork.Save();
            }
			catch (Exception)
			{

				throw;
			}
		}
		public void DeleteStock(StockDTO stock)
		{
			try
			{
				var stockDomain = _mapper.Map<Stock>(stock);
				_unitOfWork.Stock.Delete(stockDomain);
                _unitOfWork.Save();
            }
			catch (Exception)
			{

				throw;
			}
		}
		public async Task IncreaseStock(Guid product_id, int number)
		{
			try
			{
				var stock = await _unitOfWork.Stock.GetFirstOrDefaultAsync(u => u.ProductID == product_id);
				stock.Quantity += number;
                _unitOfWork.Save();
            }
			catch (Exception)
			{

				throw;
			}
		}
		public async Task DecreaseStock(Guid product_id, int number)
		{
			try
			{
				var stock = await _unitOfWork.Stock.GetFirstOrDefaultAsync(u => u.ProductID == product_id);
				stock.Quantity -= number;
                _unitOfWork.Save();
            }
			catch (Exception)
			{

				throw;
			}
		}
	}
}
