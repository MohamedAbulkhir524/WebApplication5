using AutoMapper;
using Domain.Contracts;
using Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
	public class ServicesManager: IServicesManager
	{
		private readonly Lazy<IProductServices> _ProductServices;
        public ServicesManager(IunitOfWork unitOfWork,IMapper mapper)
        {
			_ProductServices = new Lazy<IProductServices>(() => new ProductServices(unitOfWork, mapper));
        }

		public IProductServices ProductServices => _ProductServices.Value;
	}
}
