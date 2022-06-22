using MediatR;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ChallengeMeLi.Controllers.V1.TopSecret
{
	[ApiConventionType(typeof(DefaultApiConventions))]
	[Produces("application/json")]
	[Consumes("application/json")]
	[ApiController]
	public partial class TopSecretController : ControllerBase
	{
		private readonly IMediator _mediator;
		private readonly ILogger<TopSecretController> _logger;

		public TopSecretController(IMediator mediator, ILogger<TopSecretController> logger)
		{
			_mediator = mediator;
			_logger = logger;
		}
	}
}
