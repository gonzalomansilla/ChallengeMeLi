using ChallengeMeLi.Application.UseCases.V1.TopSecretSplit.Post;
using ChallengeMeLi.Application.Wrappers;
using ChallengeMeLi.Controllers.V1.TopSecretSplit.Post;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;

namespace ChallengeMeLi.Controllers.V1.TopSecretSplit
{
    public partial class TopSecretSplitController
    {
        /// <summary>
        /// API para guardar de a un satelite los datos provinientes de la nave imperial
        /// </summary>
        /// <param name="bodyRequest">
        /// Body request
        /// </param>
        /// <param name="satelliteName">
        /// Nombre del satelite
        /// </param>
        [HttpPost]
        [Route("/topsecret_split/{satelliteName}")]
        [ProducesResponseType(typeof(ResponseWrapper<string>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseWrapper<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseWrapper<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResponseWrapper<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostTopSecretAsync(string satelliteName, [FromBody] PostTopSecretSplitBodyRequest bodyRequest)
        {
            var request = new PostTopSecretSplitRequest
            {
                Name = satelliteName,
                Distance = bodyRequest.Distance,
                Message = bodyRequest.Message
            };
            var result = await _mediator.Send(request);

            return StatusCode(StatusCodes.Status201Created, result);
        }
    }
}