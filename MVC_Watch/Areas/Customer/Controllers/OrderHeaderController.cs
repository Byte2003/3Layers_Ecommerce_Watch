using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC_Watch_Business.DTO.OrderDetailDTO;
using MVC_Watch_Business.DTO.ProductDTO;
using MVC_Watch_Business.Services;
using MVC_Watch_Data.Contracts;
using MVC_Watch_Data.Models;
using System.Security.Claims;

namespace MVC_Watch_UI.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class OrderHeaderController : Controller
    {

        private readonly OrderHeaderService _orderHeaderService;
        private readonly OrderDetailService _orderDetailService;
        private readonly IMapper _mapper;

        public OrderHeaderController(OrderHeaderService orderHeaderService, IMapper mapper, OrderDetailService orderDetailService)
        {
            _orderHeaderService = orderHeaderService;
            _mapper = mapper;
            _orderDetailService = orderDetailService;
        }
        public async Task<IActionResult> Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            var orderHeaders = await _orderHeaderService.GetAllOrderHeaderAsync(u => u.AppUserID == claim.Value);

            return View(orderHeaders);
        }

        [HttpGet]
        public async Task<IActionResult> GetOrderDetails(string headerId)
        {
            Guid header_Id = Guid.Parse(headerId);
            var details = await _orderDetailService.GetAllAsync(u => u.OrderHeaderID == header_Id, includeProperties: nameof(Product));
            return PartialView("_OrderDetailModal", _mapper.Map<IEnumerable<OrderDetail>>(details));

        }
    }
}
