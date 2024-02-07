using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public OrderHeaderController(OrderHeaderService orderHeaderService, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _orderHeaderService = orderHeaderService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
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
            var details = await _unitOfWork.OrderDetail.GetAllAsync(u => u.OrderHeaderID == header_Id, includeProperties: nameof(Product));

            return PartialView("_OrderDetailModal", details);

        }
    }
}
