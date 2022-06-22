using ChallengeMeLi.Application.Exceptions;
using ChallengeMeLi.Application.FuegoDeQuasar;
using ChallengeMeLi.Application.Repositories;
using ChallengeMeLi.Application.UseCases.V1.TopSecret.Post;
using ChallengeMeLi.Application.Wrappers;
using ChallengeMeLi.Domain.Entities;
using ChallengeMeLi.Shared.MessageErrors;

using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChallengeMeLi.Application.UseCases.V1.TopSecretSplit.Get
{
	public class GetTopSecretSplitHandler : IRequestHandler<GetTopSecretSplitRequest, ResponseWrapper<PostTopSecretResponse>>
	{
		private readonly IList<string> ErrorMessages = new List<string>();
		private readonly ISatelliteRepository _satelliteRepository;
		private readonly ISatelliteDataRepository _satelliteDataRepository;
		private readonly IMessageRepository _messageRepository;

		public GetTopSecretSplitHandler(
			ISatelliteRepository satelliteRepository,
			ISatelliteDataRepository satelliteDataRepository,
			IMessageRepository messageRepository)
		{
			_satelliteRepository = satelliteRepository;
			_satelliteDataRepository = satelliteDataRepository;
			_messageRepository = messageRepository;
		}

		public async Task<ResponseWrapper<PostTopSecretResponse>> Handle(GetTopSecretSplitRequest request, CancellationToken cancellationToken)
		{
			// Solo se definieron 3 satelites
			var satellites = await _satelliteRepository.GetAllAsync();
			var messages = await GetMessagesFromSatellites(satellites);

			ValidateData(satellites, messages);

			var fuegoDeQuasar = new MainFuegoDeQuasar(satellites);

			var messageS1 = CreateListOfstringFromWordEntity(messages[0].Words.ToList());
			var messageS2 = CreateListOfstringFromWordEntity(messages[1].Words.ToList());
			var messageS3 = CreateListOfstringFromWordEntity(messages[2].Words.ToList());
			var message = fuegoDeQuasar.GetMessage(messageS1, messageS2, messageS3);

			var satellitesData = await GetSatellitesData(satellites);
			var distanceS1 = satellitesData[0].Distance;
			var distanceS2 = satellitesData[1].Distance;
			var distanceS3 = satellitesData[2].Distance;
			var position = fuegoDeQuasar.GetLocations(distanceS1, distanceS2, distanceS3);

			var response = new PostTopSecretResponse()
			{
				Position = position,
				Message = message
			};
			return new ResponseWrapper<PostTopSecretResponse>(response);
		}

		private void ValidateData(List<Satellite> satellites, List<Message> messages)
		{
			if (satellites.Count != messages.Count)
			{
				var formatMessageError = string.Format(
					MessageErrors.TOPSECRETSPLIT_DIFERENT_NUMBER_OF_SATELLITES_AND_MESSAGES,
					satellites.Count, messages.Count);
				ErrorMessages.Add(formatMessageError);
				throw new ApiException(ErrorMessages);
			}

			// Agrupa en funcion de la cantidad de elementos de cada lista
			var messageLengthGrouping = messages
				.GroupBy(s => s.Words.Count())
				.ToList();
			if (messageLengthGrouping.Count != 1)
			{
				ErrorMessages.Add(MessageErrors.TOPSECRETSPLIT_MESSAGES_ARE_NOT_SAME_LENGTH);
				throw new ApiException(ErrorMessages);
			}
		}

		private async Task<IList<SatelliteData>> GetSatellitesData(IList<Satellite> satellites)
		{
			var satellitesData = new List<SatelliteData>();

			foreach (var satellite in satellites)
			{
				var satelliteData = await _satelliteDataRepository.GetLastSatelliteDataBySatelliteNameAsync(satellite.Name);

				if (satelliteData is null)
				{
					var formatMessageError = string.Format(MessageErrors.TOPSECRETSPLIT_SATELLITE_DATA_NOTFOUND, satellite.Name);
					ErrorMessages.Add(formatMessageError);
					throw new ApiException(ErrorMessages);
				}

				satellitesData.Add(satelliteData);
			}

			return satellitesData;
		}

		private async Task<List<Message>> GetMessagesFromSatellites(List<Satellite> satellites)
		{
			var messages = new List<Message>();

			foreach (var satellite in satellites)
			{
				var messageDb = await _messageRepository.GetLastMessageBySatelliteNameAsync(satellite.Name);

				messages.Add(messageDb);
			}

			return messages;
		}

		private string[] CreateListOfstringFromWordEntity(IList<Word> words)
		{
			// Crea el mensaje en la secuencia con la que se ingresaron las distintas partes del mismo
			var message = new List<string>();

			var messageAscendingPosition = words.OrderBy(x => x.Position);

			foreach (var word in messageAscendingPosition)
			{
				message.Add(word.Text);
			}

			return message.ToArray();
		}
	}
}
