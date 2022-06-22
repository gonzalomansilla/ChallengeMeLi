using ChallengeMeLi.Application.UseCases.V1.TopSecret.Post;
using ChallengeMeLi.Application.UseCases.V1.TopSecretSplit.Get;
using ChallengeMeLi.Application.Wrappers;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System.Threading.Tasks;

namespace ChallengeMeLi.Controllers.V1.TopSecretSplit
{
	public partial class TopSecretSplitController
	{
		/// <summary>
		/// API para obtener el mensaje secreto y la ubicacion de la nave imperial
		/// </summary>
		[HttpGet]
		[Route("/topsecret_split")]
		[ProducesResponseType(typeof(ResponseWrapper<PostTopSecretResponse>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ResponseWrapper<PostTopSecretResponse>), StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(ResponseWrapper<PostTopSecretResponse>), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> GetTopSecretAsync()
		{
			var apiName = "/topsecret_split";
			_logger.LogInformation($"API Start: {apiName}");

			var result = await _mediator.Send(new GetTopSecretSplitRequest());

			_logger.LogInformation($"API Successful: {apiName}");
			return Ok(result);
		}
	}
}
