using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Excaptions
{
	public class DeliveyMethodNotFoundException(int id):NotFound_Excaptions($"No Delivery Method with id:{id} is found")
	{
	}
}
