using ChallengeMeLi.Application.UseCases.V1.TopSecret.Post;
using ChallengeMeLi.Application.Wrappers;

using MediatR;

using System.Diagnostics.CodeAnalysis;

namespace ChallengeMeLi.Application.UseCases.V1.TopSecretSplit.Post
{
    [ExcludeFromCodeCoverage]
    public class PostTopSecretSplitRequest : SatelliteRequest, IRequest<ResponseWrapper<string>>
    {
    }
}