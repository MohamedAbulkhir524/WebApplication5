using Shared.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstraction
{
	public interface IOrderService
	{
		Task<OrderResult> GetOrderByIdAsync(Guid id);
		Task<IEnumerable<OrderResult>> GetOrdersByEmailAsync(string email);
		Task<OrderResult> CreateOrderAsync(OrderRequest orderRequest, string userEmail);
		Task<IEnumerable<DeliveryMethodResult>> GetDeliveryMethodsAsync();
	}
}
