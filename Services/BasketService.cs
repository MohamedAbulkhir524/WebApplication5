using AutoMapper;
using Domain.Contracts;
using Domain.Entites;
using Domain.Excaptions;
using Services.Abstraction;
using Shared.BasketDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
	public class BasketService(IBasketRepositry basketRepository, IMapper mapper) : IBasketServices
	{
		public async Task<BasketDto> GetBasketAsync(string id)
		{
			var basket = await basketRepository.GetBasketAsync(id);

			return basket is null ? throw new BasketNotFoundExcaptions(id)
				: mapper.Map<BasketDto>(basket);
		}

		public async Task<BasketDto> UpdateBasketAsync(BasketDto basket)
		{
			var customerBasket = mapper.Map<CustomerBasket>(basket);

			var updatedBasket = await basketRepository.UpdateBasketAsync(customerBasket);

			return updatedBasket is null ? throw new Exception("Can't Update Basket Now!")
				: mapper.Map<BasketDto>(updatedBasket);
		}
	}
}
