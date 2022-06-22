using ChallengeMeLi.Controllers.V1.TopSecretSplit.Post;
using ChallengeMeLi.Tests.Common;

using System.Net.Http;
using System.Threading.Tasks;

namespace ChallengeMeLi.Tests.Client
{
	public class PostTopSecretSplitClient : BaseClient<PostTopSecretSplitBodyRequest, string>
	{
		private const string API_ENDPOINT = "/topsecret_split";

		public PostTopSecretSplitClient(HttpClient httpClient) : base(httpClient)
		{
		}

		public async Task<ResponseTest<string>> PostAsync(PostTopSecretSplitBodyRequest resquest, string id)
		{
			var response = await BasePostWithIdAsync(id, resquest, API_ENDPOINT);

			return response;
		}
	}
}
