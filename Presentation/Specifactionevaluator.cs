using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
	public static class Specifactionevaluator
	{
        public static IQueryable<T> GetQurey<T> (IQueryable<T> baseQuerey,Spacefication Spacefication) where T : class
		{
			var query = baseQuerey;
			if (Spacefication.Criteria is not null)
				query = query.Where(Spacefication.Criteria);

			query=Spacefication.Include.Aggregate(query,(currentQuerey,include)=>currentQuerey.Include(include));

			if (Spacefication.OrderBy is not null)
				query = query.OrderBy(Spacefication.OrderBy);

			else if(Spacefication.OrderByDescending is not null)
				query = query.OrderByDescending(Spacefication.OrderByDescending);


			if (Spacefication.Ispaginated)
				query=query.Skip(Spacefication.Skip).Take(Spacefication.Take);

			return query;
		}

    }
}
