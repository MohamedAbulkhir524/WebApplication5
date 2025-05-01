using AutoMapper;
using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MappingProfiles
{
	public class OrderProfile: Profile
	{
		public OrderProfile() {

		CreateMap<Address,AddressDtos>().ReverseMap();
		
		}
	}
}
