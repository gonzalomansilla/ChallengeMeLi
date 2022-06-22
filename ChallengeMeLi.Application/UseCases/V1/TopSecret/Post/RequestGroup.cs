using ChallengeMeLi.Application.Wrappers;

using MediatR;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace ChallengeMeLi.Application.UseCases.V1.TopSecret.Post
{
	[ExcludeFromCodeCoverage]
	public class PostTopSecretRequestGroup : IRequest<ResponseWrapper<PostTopSecretResponse>>
	{
		public IEnumerable<SatelliteRequest> Satellites { get; set; }
	}
}
