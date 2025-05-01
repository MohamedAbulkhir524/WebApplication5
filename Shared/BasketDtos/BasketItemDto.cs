using System.ComponentModel.DataAnnotations;

namespace Shared.BasketDtos
{
	public class BasketItemDto
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		[Range(1, double.MaxValue)]
		public decimal Price { get; set; }

		public string Brand { get; set; }

		[Range(1, 10)]
		public string Type { get; set; }

		public int Quantity { get; set; }
	}
}