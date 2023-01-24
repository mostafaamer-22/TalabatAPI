using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.BLL.Interfaces;
using Talabat.DAL.Entities;
using Talabat.Dtos;

namespace Talabat.Controllers
{

    public class BasketController : BaseControllerApi
    {
        private readonly IBasketRepository _repository;
        private readonly IMapper _mapper;

        public BasketController(IBasketRepository repository , IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("GetBasket")]
       public async  Task<ActionResult<CustomerBasket>> GetBasketById(string id)
        {
            var basket = await _repository.GetBasketAsync(id);
            return Ok(basket ?? new CustomerBasket(id));
        }

        [HttpPost("UpdateBasket")]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDto basket)
        {
            var customerBasket = _mapper.Map<CustomerBasketDto , CustomerBasket>(basket);
            var updatedBasket = await _repository.UpdateBasketAsync(customerBasket);
            return Ok(updatedBasket);

        }

        [HttpDelete("DeleteBasket")]
        public async Task<ActionResult<bool>> DeleteBasket(string id)
        {
            var deleted = await _repository.DeleteBasketAsync(id);
            return Ok(deleted);
        }
    }
}
