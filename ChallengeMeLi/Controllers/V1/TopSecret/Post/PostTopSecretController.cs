using ChallengeMeLi.Application.UseCases.V1.TopSecret.Post;
using ChallengeMeLi.Application.Wrappers;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System.Threading.Tasks;

namespace ChallengeMeLi.Controllers.V1.TopSecret
{
	public partial class TopSecretController
	{
		/// <summary>
		/// API para obtener la ubicación de la nave imperial y el mensaje que emite
		/// </summary>
		/// <param name="bodyRequest">
		/// Body request
		/// </param>
		[HttpPost]
		[Route("/topsecret")]
		[ProducesResponseType(typeof(ResponseWrapper<PostTopSecretResponse>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ResponseWrapper<PostTopSecretResponse>), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(ResponseWrapper<PostTopSecretResponse>), StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(ResponseWrapper<PostTopSecretResponse>), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> PostTopSecretAsync([FromBody] PostTopSecretRequestGroup bodyRequest)
		{
			var apiName = "/topsecret";
			_logger.LogInformation($"API Start: {apiName}");

			var result = await _mediator.Send(bodyRequest);

			_logger.LogInformation($"API Successful: {apiName}");
			return Ok(result);
		}
	}
}
