using AutoMapper;
using MVC_Watch_Business.DTO.CartItemDTO;
using MVC_Watch_Business.DTO.OrderHeaderDTO;
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
    public class OrderHeaderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public OrderHeaderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderHeaderDTO>> GetAllOrderHeaderAsync(Expression<Func<OrderHeader, bool>>? filter = null, string? includeProperties = null)
        {
            try
            {
                var headersDomain = await _unitOfWork.OrderHeader.GetAllAsync(filter, includeProperties);
                var headers = _mapper.Map<IEnumerable<OrderHeaderDTO>>(headersDomain);
                return headers;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
