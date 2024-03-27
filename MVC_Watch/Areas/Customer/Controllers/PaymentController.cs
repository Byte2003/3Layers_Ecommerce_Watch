using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC_Watch_Business.Services;
using PayPal.Api;
using System.Security.Claims;

namespace MVC_Watch_UI.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class PaymentController : Controller
    {
        private readonly PaymentService _paymentService;
        private readonly CartItemService _cartItemService;
        private readonly ProductService _productService;

        // TODO:
        // Get the current cart infor(items, totals, user) 
        // send to the service to create order
        // then delete the current cart (in the session ?)

        public PaymentController(PaymentService paymentService, CartItemService cartItemService, ProductService productService)
        {
            _paymentService = paymentService;
            _cartItemService = cartItemService;
            _productService = productService;
        }

        public async Task<IActionResult> AuthorizePayment(string Cancel = null)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            var cartItems = await _cartItemService.GetAllCartItemsAsync(u => u.AppUserID == claim.Value, includeProperties: "Product");
            foreach (var cart in cartItems)
            {
                cart.Price = await _productService.GetCurrentPriceOfProduct(cart.ProductID);
            }

            try
            {
                APIContext apiContext = _paymentService.GetAPIContext();

                if (!string.IsNullOrEmpty(Cancel))
                {
                    return View("CancelledPayment");
                }

                string payerId = HttpContext.Request.Query["PayerID"].ToString();
                if (string.IsNullOrEmpty(payerId))
                {
                    var requestUrl = HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path;
                    var guid = Convert.ToString((new Random()).Next(100000));
                    requestUrl = requestUrl + "?guid=" + guid;

                    var createdPayment = _paymentService.CreatePayment(apiContext, requestUrl, cartItems);
                    var links = createdPayment.links.GetEnumerator();
                    string paypalRedirectUrl = "";
                    while (links.MoveNext())
                    {
                        Links link = links.Current;
                        if (link.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            paypalRedirectUrl = link.href;
                            break;
                        }
                    }

                    // Store payment ID in session
                    HttpContext.Session.SetString("payment", createdPayment.id);
                    return Redirect(paypalRedirectUrl);
                }
                else
                {
                    var guid = Request.Query["guid"];
                    var paymentId = HttpContext.Session.GetString("payment");

                    // Execute payment
                    var executedPayment = _paymentService.ExecutePayment(apiContext, payerId, paymentId);
                    if (executedPayment.state.ToLower() != "approved")
                    {
                        return View("FailedPayment", "Payment was not approved. Please try again.");
                    }


                    // TODO
                    // create order and store in the db
                    _paymentService.CreateOrder(claim.Value, executedPayment);

                }
            }
            catch (PayPal.PaymentsException ex)
            {
                return View("FailedPayment", ex.ToString());
            }


            // remove cart and payment 
            foreach(var item in cartItems)
            {
                _cartItemService.DeleteCart(item);
            }
            _paymentService.EndPayment();
            HttpContext.Session.Remove("payment");
            return View("SuccessPayment");
        }
    }
}
