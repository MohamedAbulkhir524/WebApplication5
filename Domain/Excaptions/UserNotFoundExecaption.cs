using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Excaptions
{
	public class UserNotFoundExecaption(string email):NotFound_Excaptions($"user with Email:{email} not found")
	{
	}
}
