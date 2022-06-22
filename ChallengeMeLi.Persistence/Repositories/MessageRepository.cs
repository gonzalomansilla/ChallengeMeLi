using ChallengeMeLi.Application.Repositories;
using ChallengeMeLi.Domain.Entities;

using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChallengeMeLi.Persistence.Repositories
{
	public class MessageRepository : IMessageRepository
	{
		private readonly AppDbContext _context;

		public MessageRepository(AppDbContext context)
		{
			_context = context;
		}

		public async Task<bool> ExistAsync(string name)
		{
			return await _context.Messages.AnyAsync(e => e.Satellite.Name == name);
		}

		public async Task<List<Message>> GetAllAsync()
		{
			return await _context.Messages.ToListAsync();
		}

		public async Task<Message> GetByNameAsync(string name)
		{
			return await _context.Messages.FirstAsync(x => x.Satellite.Name == name);
		}

		public async Task<Message> GetLastMessageBySatelliteNameAsync(string name)
		{
			var lastMessageId = await _context.Messages
				.Where(m => m.Satellite.Name == name)
				.MaxAsync(m => m.MessageId);

			var message = await _context.Messages
				.Include(m => m.Words)
				.FirstAsync(x => x.MessageId == lastMessageId);

			return message;
		}

		public async Task InsertAsync(Message entity)
		{
			await _context.Messages.AddAsync(entity);
			await SaveChangesAsync();
		}

		public async Task SaveChangesAsync()
		{
			await _context.SaveChangesAsync();
		}
	}
}
