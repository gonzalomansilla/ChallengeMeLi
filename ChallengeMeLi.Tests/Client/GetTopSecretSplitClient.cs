using ChallengeMeLi.Application.UseCases.V1.TopSecret.Post;
using ChallengeMeLi.Application.UseCases.V1.TopSecretSplit.Get;
using ChallengeMeLi.Tests.Common;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeMeLi.Tests.Client
{
	public class GetTopSecretSplitClient : BaseClient<GetTopSecretSplitRequest, PostTopSecretResponse>
	{
		private const string API_ENDPOINT = "/topsecret_split";

		public GetTopSecretSplitClient(HttpClient httpClient) : base(httpClient)
		{
		}

		public async Task<ResponseTest<PostTopSecretResponse>> GetAsync(GetTopSecretSplitRequest request)
		{
			var response = await BaseGetAsync(request, API_ENDPOINT);

			return response;
		}
	}
}
