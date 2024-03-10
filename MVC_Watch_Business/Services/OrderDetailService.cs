using AutoMapper;
using MVC_Watch_Business.DTO.OrderDetailDTO;
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
    public class OrderDetailService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public OrderDetailService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<OrderDetailDTO>> GetAllAsync(Expression<Func<OrderDetail, bool>>? filter = null, string? includeProperties = null)
        {
            try
            {
                var orderDetailDomains =  await _unitOfWork.OrderDetail.GetAllAsync(filter, includeProperties);
                var orderDetails = _mapper.Map<IEnumerable<OrderDetailDTO>>(orderDetailDomains);
                return orderDetails;
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
