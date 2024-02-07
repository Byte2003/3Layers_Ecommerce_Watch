using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVC_Watch_Business.DTO.AppUserDTO;
using MVC_Watch_Business.DTO.CartItemDTO;
using MVC_Watch_Data.Contracts;
using MVC_Watch_Data.Models;
using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Watch_Business.Services
{
    public class PaymentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private Payment? payment;

        public PaymentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public APIContext GetAPIContext()
        {
            return PaypalConfiguration.GetAPIContext();
        }

        public Payment ExecutePayment(APIContext aPIContext, string payerId, string paymentId)
        {

            var paymentExecution = new PaymentExecution()
            {
                payer_id = payerId
            };

            payment = new Payment()
            {
                id = paymentId,
            };

            return payment.Execute(aPIContext, paymentExecution);
        }

        public Payment CreatePayment(APIContext aPIContext, string redirectUrl, IEnumerable<CartItemDTO> cartItems)
        {

            var itemList = new ItemList()
            {
                items = new List<Item>(),

            };

            double total = 0;
            foreach (var item in cartItems)
            {
                itemList.items.Add(new Item
                {
                    name = item.Product.Name,
                    currency = "USD",
                    price = item.Price.ToString(),
                    quantity = item.Quantity.ToString(),
                    sku = "sku",

                    // dont know what field to store product ID so I choose description :))
                    description = item.ProductID.ToString(),
                });
                total += item.Price * item.Quantity;
            }

            var payer = new Payer()
            {
                payment_method = "paypal",
            };

            var redirUrl = new RedirectUrls()
            {
                cancel_url = redirectUrl + "&Cancel=true",
                return_url = redirectUrl
            };

            var amount = new Amount()
            {
                currency = "USD",
                total = total.ToString()
            };

            var transactionList = new List<Transaction>
            {
                new Transaction()
                {
                    description = "Transaction Description",
                    invoice_number = Guid.NewGuid().ToString(),
                    amount = amount,
                    item_list = itemList
                }
            };

            payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrl
            };

            return payment.Create(aPIContext);
        }

        public void CreateOrder(string userId, Payment payment)
        {
            OrderHeader header = new OrderHeader();
            Guid headerId = Guid.NewGuid();
            header.Name = "Test";
            header.OrderHeaderId = headerId;
            header.AppUserID = userId;
            header.OrderDate = DateTime.Now;
            header.Address = payment.payer.payer_info.shipping_address.line1;
            header.PostalCode = payment.payer.payer_info.shipping_address.postal_code;
            header.City = payment.payer.payer_info.shipping_address.city;

            // Phone is null so cannot insert, fix tomorrow !!
            header.PhoneNumber = payment.payer.payer_info.phone + ""; // I add  " " to make sure it not null -.-

            foreach (var transaction in payment.transactions)
            {
                header.OrderTotal = double.Parse(transaction.amount.total);
            }

            _unitOfWork.OrderHeader.Add(header);

            foreach (var transaction in payment.transactions)
            {
                foreach (var item in transaction.item_list.items)
                {
                    OrderDetail detail = new OrderDetail();
                    detail.OrderHeaderID = headerId;
                    detail.ProductID = Guid.Parse(item.description);
                    detail.Quantity = int.Parse(item.quantity);
                    detail.Price = double.Parse(item.price);

                    _unitOfWork.OrderDetail.Add(detail);
                }
            }
        }

        public void EndPayment()
        {
            _unitOfWork.Save();
        }
    }
}
