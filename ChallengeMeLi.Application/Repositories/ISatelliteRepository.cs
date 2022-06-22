using ChallengeMeLi.Domain.Entities;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChallengeMeLi.Application.Repositories
{
	public interface ISatelliteRepository
	{
		Task<List<Satellite>> GetAllAsync();

		Task<Satellite> GetByNameAsync(string name);

		void Delete(Satellite entity);

		Task InsertAsync(Satellite entity);

		Task<bool> ExistAsync(string name);

		Task SaveChangesAsync();

		void Attach(Satellite entity);
	}
}
