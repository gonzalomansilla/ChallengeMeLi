using ChallengeMeLi.Application.Wrappers;
using ChallengeMeLi.Tests.Common;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ChallengeMeLi.Tests.Client
{
	public class BaseClient<R, T> where R : class where T : class
	{
		private const string APPLICATION_JSON = "application/json";
		private readonly HttpClient _httpClient;

		public BaseClient(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		#region POST

		public async Task<ResponseTest<T>> BasePostAsync(R bodyRequest, string apiEndpoint)
		{
			var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, apiEndpoint)
			{
				Content = bodyRequest is null ? null : CreateJsonRequest(bodyRequest),
			};

			var httpResponseMessage = await _httpClient.SendAsync(httpRequestMessage);

			var jsonResponse = await httpResponseMessage.Content.ReadAsStringAsync();
			var responseWrapper = GetObjectResponseFromJson(jsonResponse);

			var response = new ResponseTest<T>
			{
				ResponseWrapper = responseWrapper,
				StatusCode = httpResponseMessage.StatusCode
			};

			return response;
		}

		public async Task<ResponseTest<T>> BasePostWithIdAsync(string id, R bodyRequest, string apiEndpoint)
		{
			var formatId = Path.Combine(apiEndpoint, id);
			var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, formatId)
			{
				Content = CreateJsonRequest(bodyRequest),
			};

			var httpResponseMessage = await _httpClient.SendAsync(httpRequestMessage);

			var jsonResponse = await httpResponseMessage.Content.ReadAsStringAsync();
			var responseWrapper = GetObjectResponseFromJson(jsonResponse);

			var response = new ResponseTest<T>
			{
				ResponseWrapper = responseWrapper,
				StatusCode = httpResponseMessage.StatusCode
			};

			return response;
		}

		#endregion POST

		#region GET

		public async Task<ResponseTest<T>> BaseGetAsync(R bodyRequest, string apiEndpoint)
		{
			var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, apiEndpoint)
			{
				Content = CreateJsonRequest(bodyRequest),
			};

			var httpResponseMessage = await _httpClient.SendAsync(httpRequestMessage);

			var jsonResponse = await httpResponseMessage.Content.ReadAsStringAsync();
			var responseWrapper = GetObjectResponseFromJson(jsonResponse);

			var response = new ResponseTest<T>
			{
				ResponseWrapper = responseWrapper,
				StatusCode = httpResponseMessage.StatusCode
			};

			return response;
		}

		#endregion GET

		private StringContent CreateJsonRequest(R request)
		{
			var json = JsonSerializer.Serialize(request);
			var stringContent = new StringContent(
				json,
				Encoding.Default,
				APPLICATION_JSON);
			return stringContent;
		}

		private ResponseWrapper<T> GetObjectResponseFromJson(string jsonResponse)
		{
			return JsonSerializer.Deserialize<ResponseWrapper<T>>(
				jsonResponse,
				new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
		}
	}
}
