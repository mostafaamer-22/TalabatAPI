using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talabat.BLL.Interfaces;
using Talabat.DAL.Entities.Order;
using Talabat.Dtos;

namespace Talabat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrdersController(IOrderService orderService , IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        [HttpPost("PLaceOrder")]

        public async Task<ActionResult<Order>> PlaceOrder(OrderDto orderDto)
        {

            var email = User.FindFirstValue(ClaimTypes.Email);
            var address = _mapper.Map<UserAddressDto, Address>(orderDto.ShipToAddress);
            var order = await _orderService.CreateOrderAsync(email, orderDto.DeliveryMethodId, orderDto.BasketId, address);
            if (order is null)
                return BadRequest();
            return Ok(order);  

        }
    }
}
