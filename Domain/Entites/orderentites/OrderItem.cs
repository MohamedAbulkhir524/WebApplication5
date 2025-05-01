using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites.orderentites
{
	public class OrderItem:BaseEntity<Guid>
	{
		public ProductInorderItem Product { get; set; }
		public int Quantity { get; set; }
		public decimal Price { get; set; }

	}
}
