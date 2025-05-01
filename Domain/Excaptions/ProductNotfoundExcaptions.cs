using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Excaptions
{
	public  sealed class ProductNotfoundExcaptions:NotFound_Excaptions
	{
        public ProductNotfoundExcaptions(int id):base($"product with id{id} not found")
        {
            
        }
    }
}
