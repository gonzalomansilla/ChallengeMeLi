using ChallengeMeLi.Application.Exceptions;
using ChallengeMeLi.Application.FuegoDeQuasar;
using ChallengeMeLi.Application.Repositories;
using ChallengeMeLi.Application.Wrappers;
using ChallengeMeLi.Domain.Entities;
using ChallengeMeLi.Shared.MessageErrors;

using MediatR;

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ChallengeMeLi.Application.UseCases.V1.TopSecret.Post
{
    /*
		Clase encargada de manejar la logica del request proviniente del endpoint /topsecret
	 */

    public class PostTopSecretHandler : IRequestHandler<PostTopSecretRequestGroup, ResponseWrapper<PostTopSecretResponse>>
    {
        private readonly IList<string> ErrorMessages = new List<string>();

        private readonly ISatelliteRepository _satelliteRepository;

        public PostTopSecretHandler(ISatelliteRepository satelliteRepository)
        {
            _satelliteRepository = satelliteRepository;
        }

        public async Task<ResponseWrapper<PostTopSecretResponse>> Handle(PostTopSecretRequestGroup request, CancellationToken cancellationToken)
        {
            ValidateData(request);

            await SatellitesValidations(request);

            // Crea una lista de satelites con los datos Posicion-Distancia asociados
            var fullSatellitesData = await GetFullSatellitesData(request.Satellites.ToList());
            var fuegoDeQuasar = new MainFuegoDeQuasar(fullSatellitesData.Select(s => s.Satellite).ToList());

            var distanceS1 = fullSatellitesData[0].Distance;
            var distanceS2 = fullSatellitesData[1].Distance;
            var distanceS3 = fullSatellitesData[2].Distance;
            var position = fuegoDeQuasar.GetLocations(distanceS1, distanceS2, distanceS3);

            var satellites = request.Satellites.ToArray();
            var messageS1 = satellites[0].Message.ToArray();
            var messageS2 = satellites[1].Message.ToArray();
            var messageS3 = satellites[2].Message.ToArray();
            var message = fuegoDeQuasar.GetMessage(messageS1, messageS2, messageS3);

            var response = new PostTopSecretResponse()
            {
                Position = position,
                Message = message
            };
            return new ResponseWrapper<PostTopSecretResponse>(response);
        }

        private async Task<IList<SatelliteData>> GetFullSatellitesData(IList<SatelliteRequest> satellites)
        {
            var satellitesDb = new List<SatelliteData>();

            foreach (var satellite in satellites)
            {
                var satelliteDb = await _satelliteRepository.GetByNameAsync(satellite.Name);

                var satelliteData = new SatelliteData
                {
                    Distance = decimal.Parse(satellite.Distance),
                    Satellite = satelliteDb
                };

                satellitesDb.Add(satelliteData);
            }

            return satellitesDb;
        }

        private async Task SatellitesValidations(PostTopSecretRequestGroup request)
        {
            foreach (var satellite in request.Satellites)
            {
                var existSatellite = await _satelliteRepository.ExistAsync(satellite.Name);

                if (!existSatellite)
                {
                    ErrorMessages.Add(string.Format(MessageErrors.TOPSECRET_SATELLITE_NOTFOUND, satellite.Name));
                    throw new ApiException(ErrorMessages);
                }
            }
        }

        private void ValidateData(PostTopSecretRequestGroup request)
        {
            if (request.Satellites.Count() != 3)
            {
                ErrorMessages.Add(MessageErrors.TOPSECRET_ONLY_THREE_SATELLITES);
                throw new ApiException(ErrorMessages);
            }

            // Agrupa en funcion de la cantidad de elementos de cada lista
            var messageLengthGrouping = request.Satellites
                .GroupBy(s => s.Message.Count)
                .ToList();
            if (messageLengthGrouping.Count != 1)
            {
                ErrorMessages.Add(MessageErrors.TOPSECRET_MESSAGES_ARE_NOT_SAME_LENGTH);
                throw new ApiException(ErrorMessages);
            }

            // Busca elementos nulos en cada lista de mensajes
            var messagesHaveNullElements = request.Satellites
                .Any(e => e.Message.Any(x => x is null));
            if (messagesHaveNullElements)
            {
                ErrorMessages.Add(MessageErrors.TOPSECRET_MESSAGE_CONTAIN_NULL_ELEMENT);
                throw new ApiException(ErrorMessages);
            }
        }
    }
}