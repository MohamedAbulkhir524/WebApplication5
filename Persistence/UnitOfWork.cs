using Domain.Contracts;
using Domain.Entites;
using Persistence.Data;
using Persistence.Repositres;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
	public class UnitOfWork : IunitOfWork
	{
		private readonly StoreDbContext _context;
		private ConcurrentDictionary<string, object> _repositories;
        public UnitOfWork(StoreDbContext context)
        {
			_context = context;
			_repositories = new();
        }
        public IGenericRepoitory<TEntity, Tkey> GetRepoitory<TEntity, Tkey>() where TEntity : BaseEntity<Tkey>
		
			=>(IGenericRepoitory<TEntity, Tkey>)_repositories.GetOrAdd(typeof(TEntity).Name, _ => new GenericRepository<TEntity, Tkey?>(_context));
		

		public async Task<int> SaveChangesAsync()
		=> await _context.SaveChangesAsync();
	}
}
