using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Excaptions
{
	public sealed class BasketNotFoundExcaptions(string id): NotFound_Excaptions($"Basket with id{id} not found")
	{
		
	}
}
