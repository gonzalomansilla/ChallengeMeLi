using MediatR;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ChallengeMeLi.Controllers.V1.TopSecretSplit
{
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ApiController]
    public partial class TopSecretSplitController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<TopSecretSplitController> _logger;

        public TopSecretSplitController(IMediator mediator, ILogger<TopSecretSplitController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
    }
}