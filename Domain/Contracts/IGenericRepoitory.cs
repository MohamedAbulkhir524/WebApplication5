using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
	public interface IGenericRepoitory<TEntity,Tkey> where TEntity:BaseEntity<Tkey>
	{
		Task<TEntity?> GetAysnc(Tkey id);
		Task<TEntity?> GetAysnc(Spaceifcation<TEntity> spaceifcation);

		Task<IEnumerable<TEntity>> GetAllAysnc(bool isTrackable=false);
		Task<IEnumerable<TEntity>> GetAllAysnc(Spaceifcation<TEntity> spaceifcation);

		Task AddAsync(TEntity entity);

		void Update (TEntity entity);

		void Delete(TEntity entity);
			
	}
}
