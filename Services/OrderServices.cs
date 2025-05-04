using AutoMapper;
using Domain.Contracts;
using Domain.Entites;
using Domain.Entites.orderentites;
using Domain.Excaptions;
using Services.Abstraction;
using Shared.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
	public class OrderServices(IUnitOfWork unitOfWork, IMapper mapper, IBasketRepository basketRepository) : IOrderService
	{
		public async Task<OrderResult> CreateOrderAsync(OrderRequest orderRequest, string userEmail)
		{
			// Address
			var address = mapper.Map<Address>(orderRequest.ShippingAddress);

			// Basket
			var basket = await basketRepository.GetBasketAsync(orderRequest.BasketId);

			if (basket is null)
				throw new BasketNotFoundExcaptions(orderRequest.BasketId);

			var orderItems = new List<OrderItem>();
			foreach (var item in basket.Items)
			{
				var product = await unitOfWork.GetRepository<Product, int>().GetAsync(item.Id);
				if (product is null)
					throw new ProductNotfoundExcaptions(item.Id);

				var productInOrderItem = new ProductInOrderItem(product.Id, product.Name, product.PictureUrl);
				var orderItem = new OrderItem(productInOrderItem, item.Quantity, product.Price);
				orderItems.Add(orderItem);
			}

			var deliveryMethod = await unitOfWork.GetRepository < DeliveryMethod, int().GetAsync(OrderResult.DeliveryMethod);
			if (deliveryMethod is null)

				throw new DeliveyMethodNotFoundException(orderRequest.DeliveryMethodId);


			var subtotal = orderItems.Sum(Item => Item.Price * Item.Quantity);

			var order = new Order(buyerEmail, address, orderItems, deliveryMethod, subtotal);

			await unitOfWork.GetRepository<Order, Guid>().AddAsync(order);

			await unitOfWork.SaveChangesAsync();

			return mapper.Map<OrderResult>(order);





		}
			
				public Task<IEnumerable<DeliveryMethodResult>> GetDeliveryMethodsAsync()
				{
			       var deliveryMethods = await unitOfWork.GetRepository<DeliveryMethod, int>().GetAllAsync();
			      return mapper.Map<IEnumerable<DeliveryMethodResult>>(deliveryMethods);
		        }

				public Task<OrderResult> GetOrderByIdAsync(Guid id)
				{
			      var orderSpecs = new OrderWithIncludeSpecification(id);
			       var order = await unitOfWork.GetRepository<Order, Guid>().GetAsync(orderSpecs);

			      if (order is null)
				    throw new OrderNotFoundException(id);

			      return mapper.Map<OrderResult>(order);
		        }

				public Task<IEnumerable<OrderResult>> GetOrdersByEmailAsync(string email)
				{


			         var orderSpecs = new OrderWithIncludeSpecification(email);
			         var orders = await unitOfWork.GetRepository<Order, Guid>().GetAllAsync(orderSpecs);
			          return mapper.Map<IEnumerable<OrderResult>>(orders);

		        }




			   }
    }  	 
