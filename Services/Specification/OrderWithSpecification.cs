using Domain.Contracts;
using Domain.Entites.orderentites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specification
{
	public class OrderWithSpecification : Spaceifcation<Order>
	{
		public OrderWithSpecification(Guid id)
			: base(order => order.Id == id)
		{
			

	        AddInclude(x => x.OrderItems);
			AddInclude(x => x.DeliveryMethod);
		}

		public OrderWithSpecification(string email)
			: base(order => order.BuyerEmail == email)
		{


			AddInclude(x => x.OrderItems);
			AddInclude(x => x.DeliveryMethod);

			SetOrderBy(x => x.OrderDate);
		}




	}
}
