using ChallengeMeLi.Domain.Entities;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChallengeMeLi.Application.Repositories
{
	public interface IMessageRepository
	{
		Task<List<Message>> GetAllAsync();

		Task<Message> GetByNameAsync(string name);

		Task<Message> GetLastMessageBySatelliteNameAsync(string name);

		Task InsertAsync(Message entity);

		Task<bool> ExistAsync(string name);

		Task SaveChangesAsync();
	}
}
