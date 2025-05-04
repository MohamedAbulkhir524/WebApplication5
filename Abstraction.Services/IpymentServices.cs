using Shared.BasketDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstraction
{
	public interface IpymentServices
	{
		Task<BasketDto> CreateOrUpdatePaymentIntentAsync(string basketId);
		Task UpdateOrderPaymentStatusAsync(string request, string stripeHeader);
	}
}
