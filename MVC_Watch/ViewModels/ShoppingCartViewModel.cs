using MVC_Watch_Business.DTO.CartItemDTO;
using MVC_Watch_Business.DTO.OrderHeaderDTO;
using MVC_Watch_Data.Models;

namespace MVC_Watch_UI.ViewModels
{
	public class ShoppingCartViewModel
	{
        public IEnumerable<CartItemDTO> CartItems { get; set; }

		public OrderHeaderDTO OrderHeader { get; set; }

    }
}
