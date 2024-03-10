using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC_Watch_Business.DTO.CartItemDTO;
using MVC_Watch_Business.Services;
using MVC_Watch_UI.ViewModels;
using System.Security.Claims;

namespace MVC_Watch_UI.Areas.Customer.Controllers
{
	[Area("Customer")]
	[Authorize]
	public class CartController : Controller
	{
		private readonly CartItemService _cartItemService;
		private readonly ProductService _productService;
		private readonly IMapper _mapper;
		[BindProperty]
		public ShoppingCartViewModel ShoppingCartViewModel { get; set; }
		public static double OrderTotal { get; set; }
		public CartController(CartItemService cartItemService, IMapper mapper, ProductService productService)
		{
			_cartItemService = cartItemService;
			_mapper = mapper;
			_productService = productService;
		}
		public async Task<IActionResult> AddToCart(Guid productID)
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);


			// Get current cartItem of User for this product
			var cart = await _cartItemService.GetCartItemAsync(u => u.AppUserID == claim.Value && u.ProductID == productID);
			if (cart == null)
			{
				AddCartItemDTO addCartItemDTO = new AddCartItemDTO()
				{
					ProductID = productID,
					AppUserID = claim.Value,
					Price = await _productService.GetCurrentPriceOfProduct(productID),
					Quantity = 1
				};
				_cartItemService.AddCartItem(addCartItemDTO);
			}
			else
			{
				await _cartItemService.IncreaseQuantity(cart.CartItemID, 1);
			}
			return RedirectToAction("Index");
		}
		public async Task<IActionResult> Index()
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

			// Retrive all cart items
			var cartItems = await _cartItemService.GetAllCartItemsAsync(u => u.AppUserID == claim.Value, includeProperties: "Product");

			ShoppingCartViewModel = new ShoppingCartViewModel()
			{
				CartItems = cartItems,
				OrderHeader = new()
			};

			// Calculate Total
			foreach (var cart in ShoppingCartViewModel.CartItems)
			{
				cart.Price = await _productService.GetCurrentPriceOfProduct(cart.ProductID);
				ShoppingCartViewModel.OrderHeader.OrderTotal += cart.Price * cart.Quantity;
			}			
			return View(ShoppingCartViewModel);
		}
		private async Task<double> GetOrderTotal()
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

			var cartItems = await _cartItemService.GetAllCartItemsAsync(u => u.AppUserID == claim.Value, includeProperties: "Product");
			double total = 0;
			foreach (var cart in cartItems)
			{
				cart.Price = await _productService.GetCurrentPriceOfProduct(cart.ProductID);
				total += cart.Price * cart.Quantity;	
			}
			return total;
		}
		#region API Calls
		[HttpPost]
		public async Task<IActionResult> UpdateCart(Guid cart_id, int quantity)
		{
			await _cartItemService.UpdateQuantity(cart_id, quantity);
			var cart = await _cartItemService.GetCartItemAsync(u => u.CartItemID == cart_id);
			var newPrice = await _productService.GetCurrentPriceOfProduct(cart.ProductID) * quantity;
			cart.Price = newPrice;
			var orderTotal = await GetOrderTotal();
			return Json(new { success = true, message = "Update successfully", price = newPrice, total = orderTotal });
		}
		[HttpDelete]
		public async Task<IActionResult> RemoveCart(Guid cart_id)
		{
			var cart = await _cartItemService.GetCartItemAsync(i => i.CartItemID == cart_id);
			_cartItemService.DeleteCart(cart);
			return Json(new { success = true, message = "Delete successfully" });
		}
		#endregion

	}
}
