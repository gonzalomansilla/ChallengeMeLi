using ChallengeMeLi.Application.UseCases.V1.TopSecret.Post;
using ChallengeMeLi.Tests.Common;

using System.Net.Http;
using System.Threading.Tasks;

namespace ChallengeMeLi.Tests.Client
{
    public class TopSecretClient : BaseClient<PostTopSecretRequestGroup, PostTopSecretResponse>
    {
        private const string API_ENDPOINT = "/topsecret";

        public TopSecretClient(HttpClient httpClient) : base(httpClient)
        {
        }

        public async Task<ResponseTest<PostTopSecretResponse>> PostTopSecretAsync(PostTopSecretRequestGroup resquest)
        {
            var response = await BasePostAsync(resquest, API_ENDPOINT);

            return response;
        }
    }
}