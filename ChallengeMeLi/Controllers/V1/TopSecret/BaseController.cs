using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace ChallengeMeLi.Controllers.V1.TopSecret
{
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ApiController]
    public partial class TopSecretController : ControllerBase
    {
        private IMediator _mediator;

        public TopSecretController(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}