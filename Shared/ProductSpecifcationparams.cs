using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
	public class ProductSpecifcationparams
	{
        private const int MaxPagesize = 10;
		private const int defaultPagesize = 10;
		public int? BrandId { get; set; }

        public int? TypeId { get; set; }

        public string? Search { get; set; }

        public string ?Sorting { get; set; }

        public int Pageindex { get; set; } = 1;

		private int _pagesize=defaultPagesize;

		public int pagesize
		{
			get => _pagesize;
			set => _pagesize = value > MaxPagesize ? MaxPagesize : value;
		}




	}
}
