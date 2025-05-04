using AutoMapper;
using Domain.Entites;
using Domain.Entites.orderentites;
using Shared.OrderDtos;
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


			CreateMap<Address, Shared.OrderDtos.AddressDto>().ReverseMap();
			CreateMap<OrderItem, OrderItemDto>()
				.ForMember(dest => dest.ProductId, options => options.MapFrom(src => src.Product.ProductId))
				.ForMember(dest => dest.ProductName, options => options.MapFrom(src => src.Product.Name))
				.ForMember(dest => dest.PictureUrl, options => options.MapFrom(src => src.Product.PictureUrl));
			CreateMap<Order, OrderResult>()
				.ForMember(dest => dest.PaymentStatus, options => options.MapFrom(src => src.PaymentStatus.ToString()))
				.ForMember(dest => dest.DeliveryMethod, options => options.MapFrom(src => src.DeliveryMethod.ShortName))
				.ForMember(dest => dest.Total, options => options.MapFrom(src => src.Subtotal + src.DeliveryMethod.Price));

			CreateMap<DeliveryMethod, DeliveryMethodResult>();


		}
	}
}
