using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites.orderentites
{
	public class Order:BaseEntity<Guid>
	{
        public string BuyerEmail { get; set; }

        public Address ShippingAddress { get; set; }

		public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        public Paymentstatus orederPaymentstatus { get; set; } = Paymentstatus.pending;

		public DeliveryMethod DeliveryMethod { get; set; }
		public int? DeliveryMethodId { get; set; }
		public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
		public decimal Subtotal { get; set; }
	}
}
