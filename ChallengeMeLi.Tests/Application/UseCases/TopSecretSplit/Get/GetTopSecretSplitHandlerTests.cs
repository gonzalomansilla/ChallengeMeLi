using ChallengeMeLi.Application.UseCases.V1.TopSecretSplit.Get;
using ChallengeMeLi.Controllers.V1.TopSecretSplit.Post;
using ChallengeMeLi.Domain.Helpers;
using ChallengeMeLi.Shared.MessageErrors;
using ChallengeMeLi.Tests.Client;
using ChallengeMeLi.Tests.Common;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

using Xunit;

namespace ChallengeMeLi.Tests.Application.UseCases.TopSecretSplit.Get
{
	/*
     * Test de integracion para la api Get TopSecretSplit
     * **/

	public class GetTopSecretSplitHandlerTests : IDisposable
	{
		protected readonly TestServer _testServer;
		private readonly GetTopSecretSplitClient _getSecretSplitClient;
		private readonly PostTopSecretSplitClient _postSecretSplitClient;

		private GetTopSecretSplitRequest _request;

		public GetTopSecretSplitHandlerTests()
		{
			var webBuilder = new WebHostBuilder()
				.UseStartup<StartupTest>();

			_testServer = new TestServer(webBuilder);

			var testServerClient = _testServer.CreateClient();
			_getSecretSplitClient = new GetTopSecretSplitClient(testServerClient);
			_postSecretSplitClient = new PostTopSecretSplitClient(testServerClient);

			_request = new GetTopSecretSplitRequest();
		}

		public void Dispose()
		{
			_testServer.Dispose();
		}

		#region Test Handler

		[Fact]
		public async Task Handler_Happy_Path_Should_Return_Ok()
		{
			// Arrange

			// Act
			var response = await _getSecretSplitClient.GetAsync(_request);

			// Assert
			Assert.NotNull(response.ResponseWrapper.Data);
			Assert.NotNull(response.ResponseWrapper.Data.Position);
			Assert.Equal(MockData.POS_X_RESULT, Helpers.RoundWithTwoDecimals(response.ResponseWrapper.Data.Position.X));
			Assert.Equal(MockData.POS_Y_RESULT, Helpers.RoundWithTwoDecimals(response.ResponseWrapper.Data.Position.Y));
			Assert.Equal(MockData.FINAL_MESSAGE, response.ResponseWrapper.Data.Message);
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
		}

		/*
		 * ** ACLARACIONES **
		 *
		 * La mayoria de test case para esta api no se realizaron ya que requeria una persistencia de datos a traves
		 * de la ejecucion que no se pudo conseguir.
		 *
		 * CASO 1: El numero de satelites y de mensajes es distinto
		 * CASO 2: El mensaje de uno de los satelites es más largo que el de los otros ingresados.
		 * CASO 3: Los datos de distancia y el mensaje de uno de los satelites no se encontraron
		 *
		 * **/

		// CASE 1

		/*
			[Fact]
			public async Task Handler_Diferent_Number_Of_Satellites_And_Messages_Should_Return_Error()
			{
				// Arrange
				var requestPost = new PostTopSecretSplitBodyRequest()
				{
					Distance = MockData.Satellite1RequestOk.Distance,
					Message = new List<string>() { "Short", "Message" },
				};
				await _postSecretSplitClient.PostAsync(requestPost, MockData.Satellite1RequestOk.Name);

				// Act
				var response = await _getSecretSplitClient.GetAsync(_request);

				// Assert
				Assert.True(response.ResponseWrapper.Errors.Contains(MessageErrors.TOPSECRETSPLIT_DIFERENT_NUMBER_OF_SATELLITES_AND_MESSAGES));
				Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
			}
		*/

		// CASE 2
		/*
		[Fact]
		public async Task Handler_One_Message_Have_Different_Length_Should_Return_Error()
		{
			// Arrange
			var requestPost = new PostTopSecretSplitBodyRequest()
			{
				Distance = MockData.Satellite1RequestOk.Distance,
				Message = new List<string>() { "Short", "Message" },
			};
			await _postSecretSplitClient.PostAsync(requestPost, MockData.Satellite1RequestOk.Name);

			// Act
			var response = await _getSecretSplitClient.GetAsync(_request);

			// Assert
			Assert.True(response.ResponseWrapper.Errors.Contains(MessageErrors.TOPSECRETSPLIT_MESSAGES_ARE_NOT_SAME_LENGTH));
			Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
		}
		*/

		// CASE 3
		/*
		[Fact]
		public async Task Handler_Satellite_Data_Not_Found_Should_Return_Error()
		{
			// Arrange

			// Act
			var response = await _getSecretSplitClient.GetAsync(_request);

			// Assert
			Assert.True(response.ResponseWrapper.Errors.Contains(MessageErrors.TOPSECRETSPLIT_SATELLITE_DATA_NOTFOUND));
			Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
		}
		*/

		#endregion Test Handler
	}
}
