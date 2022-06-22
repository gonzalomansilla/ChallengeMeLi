using ChallengeMeLi.Application.Repositories;
using ChallengeMeLi.Domain.Entities;
using ChallengeMeLi.Tests.Common;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChallengeMeLi.Tests.Repository
{
	public class MessageRepositoryTest : IMessageRepository
	{
		private readonly IList<Message> Messages = new List<Message>();

		public MessageRepositoryTest()
		{
			var message1 = new Message
			{
				MessageId = 1,
				Satellite = MockData.SatellitesDB[0],
				Words = MockData.WordsMessage1
			};
			Messages.Add(message1);

			var message2 = new Message
			{
				MessageId = 2,
				Satellite = MockData.SatellitesDB[1],
				Words = MockData.WordsMessage2
			};
			Messages.Add(message2);

			var message3 = new Message
			{
				MessageId = 3,
				Satellite = MockData.SatellitesDB[2],
				Words = MockData.WordsMessage3
			};
			Messages.Add(message3);
		}

		public async Task<bool> ExistAsync(string name)
		{
			return await Task.FromResult(Messages.Any(e => e.Satellite.Name == name));
		}

		public async Task<List<Message>> GetAllAsync()
		{
			return await Task.FromResult(Messages.ToList());
		}

		public async Task<Message> GetByNameAsync(string name)
		{
			return await Task.FromResult(Messages.First(e => e.Satellite.Name.Equals(name)));
		}

		public async Task<Message> GetLastMessageBySatelliteNameAsync(string name)
		{
			var lastMessageId = await Task.FromResult(
				Messages
					.Where(m => m.Satellite.Name == name)
					.Max(m => m.MessageId));

			var message = await Task.FromResult(
				Messages.First(x => x.MessageId == lastMessageId));

			return message;
		}

		public async Task InsertAsync(Message entity)
		{
			var lastMessageId = await Task.FromResult(
				Messages
					.Where(m => m.Satellite.Name == entity.Satellite.Name)
					.Max(m => m.MessageId));

			entity.MessageId = lastMessageId + 1;
			Messages.Add(entity);
			await Task.Delay(1);
		}

		public async Task SaveChangesAsync()
		{
			throw new NotImplementedException();
		}
	}
}
