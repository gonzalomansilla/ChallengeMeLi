using ChallengeMeLi.Domain.Common;

using System.Diagnostics.CodeAnalysis;

namespace ChallengeMeLi.Application.UseCases.V1.TopSecret.Post
{
	[ExcludeFromCodeCoverage]
	public class PostTopSecretResponse
	{
		public Position Position { get; set; }
		public string Message { get; set; }
	}
}
