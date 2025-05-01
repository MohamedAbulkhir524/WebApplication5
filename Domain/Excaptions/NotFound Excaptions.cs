using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Excaptions
{
	public class NotFound_Excaptions:Exception
	{
        public NotFound_Excaptions( string message):base(message) 
        {
            
        }
    }
}
