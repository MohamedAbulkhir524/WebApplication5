using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
	public interface IunitOfWork
	{
		public Task<int> SaveChangesAsync();

		IGenericRepoitory<TEntity, Tkey> GetRepoitory<TEntity, Tkey>() where TEntity : BaseEntity<Tkey>;
	}
}
