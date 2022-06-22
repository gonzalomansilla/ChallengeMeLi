using ChallengeMeLi.Application.UseCases.V1.TopSecret.Post;
using ChallengeMeLi.Application.Wrappers;

using MediatR;

namespace ChallengeMeLi.Application.UseCases.V1.TopSecretSplit.Get
{
    public class GetTopSecretSplitRequest : IRequest<ResponseWrapper<PostTopSecretResponse>>
    {
    }
}