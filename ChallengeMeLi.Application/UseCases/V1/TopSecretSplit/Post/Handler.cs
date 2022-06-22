using ChallengeMeLi.Application.Exceptions;
using ChallengeMeLi.Application.Repositories;
using ChallengeMeLi.Application.Wrappers;
using ChallengeMeLi.Domain.Entities;
using ChallengeMeLi.Shared.MessageErrors;

using MediatR;

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ChallengeMeLi.Application.UseCases.V1.TopSecretSplit.Post
{
	/*
        Clase encargada de manejar la logica del request proviniente del endpoint /topsecret_split
    */

	public class PostTopSecretSplitHandler : IRequestHandler<PostTopSecretSplitRequest, ResponseWrapper<string>>
	{
		private readonly IList<string> ErrorMessages = new List<string>();
		private readonly ISatelliteRepository _satelliteRepository;
		private readonly ISatelliteDataRepository _satelliteDataRepository;
		private readonly IMessageRepository _messageRepository;

		public PostTopSecretSplitHandler(
			ISatelliteRepository satelliteRepository,
			ISatelliteDataRepository satelliteDataRepository,
			IMessageRepository messageRepository)
		{
			_satelliteRepository = satelliteRepository;
			_satelliteDataRepository = satelliteDataRepository;
			_messageRepository = messageRepository;
		}

		public async Task<ResponseWrapper<string>> Handle(PostTopSecretSplitRequest request, CancellationToken cancellationToken)
		{
			ValidationData(request);

			var satelliteDb = await ValidateAndGetSatellite(request);

			await SaveData(request, satelliteDb);

			return new ResponseWrapper<string>($"The data for the satellite '{request.Name}' was created successed");
		}

		private async Task SaveData(PostTopSecretSplitRequest request, Satellite satelliteDb)
		{
			var satelliteData = new SatelliteData
			{
				Distance = decimal.Parse(request.Distance),
				Satellite = satelliteDb
			};
			await _satelliteDataRepository.InsertAsync(satelliteData);

			var words = request.Message
				.Select((m, index) => new Word { Position = index + 1, Text = m })
				.ToList();
			var message = new Message
			{
				Satellite = satelliteDb,
				Words = words
			};
			await _messageRepository.InsertAsync(message);
		}

		private async Task<Satellite> ValidateAndGetSatellite(PostTopSecretSplitRequest request)
		{
			var satelliteDb = await _satelliteRepository.GetByNameAsync(request.Name);
			if (satelliteDb is null)
			{
				ErrorMessages.Add(string.Format(MessageErrors.TOPSECRET_SATELLITE_NOTFOUND, request.Name));
				throw new ApiException(ErrorMessages);
			}

			return satelliteDb;
		}

		private void ValidationData(PostTopSecretSplitRequest request)
		{
			var messageHaveNullElements = request.Message.Any(e => e is null);
			if (messageHaveNullElements)
			{
				ErrorMessages.Add(MessageErrors.TOPSECRET_MESSAGE_CONTAIN_NULL_ELEMENT);
				throw new ApiException(ErrorMessages);
			}
		}
	}
}
