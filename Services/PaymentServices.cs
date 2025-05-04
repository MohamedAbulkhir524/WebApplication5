using AutoMapper;
using Domain.Contracts;
using Domain.Entites.orderentites;
using Domain.Excaptions;
using Microsoft.Extensions.Configuration;
using Services.Abstraction;
using Shared.BasketDtos;
using Shared.OrderDtos;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
	public class PaymentServices(IUnitOfWork unitOfWork, IBasketRepository basketRepository, IMapper mapper,IConfiguration configuration) : IpymentServices
	{
		public Task<BasketDto> CreateOrUpdatePaymentIntentAsync(string basketId)
		{
			StripeConfiguration.ApiKey = configuration.GetRequiredSection("Stripe")["SecretKey"];

			var basket = await basketRepository.GetBasketAsync(basketId);

			if (basket is null)
				throw new BasketNotFoundException(basketId);

			foreach (var item in basket.Items)
			{
				var product = await productRepo.GetAsync(item.Id);
				if (product is null)
					throw new ProductNotFoundException(item.Id);

				item.Price = product.Price;
			}

			var deliveryMethod = await unitOfWork.GetRepository < DeliveryMethod, int().GetAsync(OrderResult.DeliveryMethod);
			if (deliveryMethod is null)

				throw new DeliveyMethodNotFoundException(basket.DeliveryMethodId.value);

			basket.shippingPrice = deliveryMethod.price;

			var amount = (long)(basket.Items.Sum(i => i.Quantity * i.Price) + basket.ShippingPrice)*100;

			var service = new PaymentIntentService();

			if (string.IsNullOrWhiteSpace(basket.PaymentIntentId)) // Create
			{
				var options = new PaymentIntentCreateOptions
				{
					Amount = amount,
					PaymentMethodTypes = ["card"],
					Currency = "USD"
				};
				var paymentIntent = await service.CreateAsync(options);
				basket.PaymentIntentId = paymentIntent.Id;
				basket.ClientSecret = paymentIntent.ClientSecret;
			}
			else
			{
				// Update logic here

				var options = new PaymentIntentUpdateOptions
				{
					Amount = amount
				};

				await service.UpdateAsync(basket.PaymentIntentId, options);
				await basketRepository.UpdateBasketAsync(basket);
				return mapper.Map<BasketDto>(basket);
			}
		}

		public Task UpdateOrderPaymentStatusAsync(string request, string stripeHeader)
		{
			throw new NotImplementedException();
		}
	}
}
