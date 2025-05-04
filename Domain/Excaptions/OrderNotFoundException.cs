using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Excaptions
{
	public class OrderNotFoundException(Guid id):NotFound_Excaptions($"NO orderwith Id:{id} was Found")
	{


	}
}
