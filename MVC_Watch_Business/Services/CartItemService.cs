
using AutoMapper;
using MVC_Watch_Business.DTO.CartItemDTO;
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
	public class CartItemService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
        public CartItemService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
			_mapper = mapper;
        }
        public async Task<IEnumerable<CartItemDTO>> GetAllCartItemsAsync(Expression<Func<CartItem, bool>>? filter = null, string? includeProperties = null)
        {
			try
			{
				var cartsDomain = await _unitOfWork.CartItem.GetAllAsync(filter, includeProperties);
				var carts = _mapper.Map<IEnumerable<CartItemDTO>>(cartsDomain);
				return carts;
			}
			catch (Exception)
			{

				throw;
			}
        }
		public async Task<CartItemDTO> GetCartItemAsync(Expression<Func<CartItem, bool>> filter = null, string? includeProperties = null)
		{
			try
			{
				var cartDomain = await _unitOfWork.CartItem.GetFirstOrDefaultAsync(filter, includeProperties);
				var cart = _mapper.Map<CartItemDTO>(cartDomain);
				return cart;
			}
			catch (Exception)
			{

				throw;
			}
		}
		public void AddCartItem(AddCartItemDTO cart)
		{
			try
			{
				var cartDomain = _mapper.Map<CartItem>(cart);
				_unitOfWork.CartItem.Add(cartDomain);
                _unitOfWork.Save();

            }
            catch (Exception)
			{

				throw;
			}
		}
		public void UpdateCart(UpdateCartItemDTO cart)
		{
			try
			{
				var cartDomain = _mapper.Map<CartItem>(cart);
				_unitOfWork.CartItem.Update(cartDomain);
				_unitOfWork.Save();
			}
			catch (Exception)
			{

				throw;
			}
		}
		public void DeleteCart(CartItemDTO cart)
		{
			try
			{
				var cartDomain = _mapper.Map<CartItem>(cart);
				_unitOfWork.CartItem.Delete(cartDomain);
				_unitOfWork.Save();
			}
			catch (Exception)
			{

				throw;
			}
		}
		public async Task IncreaseQuantity(Guid cartID, int quantity)
		{
			var cartDomain = await _unitOfWork.CartItem.GetFirstOrDefaultAsync( i => i.CartItemID == cartID );
			cartDomain.Quantity += quantity;
			_unitOfWork.CartItem.Update(cartDomain); // EF Core tracking are set to false in Program.cs
            _unitOfWork.Save();
        }
        public async Task DecreaseQuantity(Guid cartID, int quantity)
		{
			var cartDomain = await _unitOfWork.CartItem.GetFirstOrDefaultAsync(u => u.CartItemID == cartID);
			cartDomain.Quantity -= quantity;
			_unitOfWork.CartItem.Update(cartDomain); // EF Core tracking are set to false in Program.cs
            _unitOfWork.Save();
        }
		public async Task UpdateQuantity(Guid cartID,int quantity)
		{
			var cartDomain = await _unitOfWork.CartItem.GetFirstOrDefaultAsync(u => u.CartItemID == cartID);
			cartDomain.Quantity = quantity;
			_unitOfWork.CartItem.Update(cartDomain); // EF Core tracking are set to false in Program.cs
            _unitOfWork.Save();

        }
	}
}
