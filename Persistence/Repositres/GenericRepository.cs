using Domain.Contracts;
using Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositres
{
	public class GenericRepository<TEntity, Tkey> : IGenericRepoitory<TEntity, Tkey>
		where TEntity : BaseEntity<Tkey>
	{
		private readonly StoreDbContext _context;
        public GenericRepository(StoreDbContext context)
        {
			_context = context;
        }
        public async Task AddAsync(TEntity entity)
					=>await _context.AddAsync(entity);


		public void Delete(TEntity entity)

			=>  _context.Set<TEntity>().Remove(entity);
		

		public async Task<IEnumerable<TEntity>> GetAllAysnc(bool isTrackable = false)
		{
			if (isTrackable)
			{
				return await _context.Set<TEntity>().ToListAsync();
			}

			else
			{
				return await _context.Set<TEntity>().AsNoTracking().ToListAsync();
			}
		}

		public async Task<IEnumerable<TEntity>> GetAllAysnc(Spaceifcation<TEntity> spaceifcation)
		{
			throw new NotImplementedException();
		}
		public Task<TEntity?> GetAysnc(Spaceifcation<TEntity> spaceifcation)
		{
			throw new NotImplementedException();
		}


		public async Task<TEntity?> GetAysnc(Tkey id)
		=> await _context.Set<TEntity>().FindAsync(id);

		public void Update(TEntity entity)
		
			=> _context.Update(entity);
		
	}
}
