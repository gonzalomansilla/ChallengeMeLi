using ChallengeMeLi.Controllers.V1.TopSecretSplit.Post;
using ChallengeMeLi.Domain.Common;
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

namespace ChallengeMeLi.Tests.Application.UseCases.TopSecretSplit.Post
{
	/*
     * Test de integracion para la api Post TopSecretSplit
     * **/

	public class PostTopSecretSplitHandlerTests : IDisposable
	{
		protected readonly TestServer _testServer;
		private const string EMPTY_STRING = " ";
		private readonly PostTopSecretSplitClient _topSecretSplitClient;
		private PostTopSecretSplitBodyRequest _request;

		public PostTopSecretSplitHandlerTests()
		{
			var webBuilder = new WebHostBuilder()
				.UseStartup<StartupTest>();

			_testServer = new TestServer(webBuilder);

			_topSecretSplitClient = new PostTopSecretSplitClient(_testServer.CreateClient());
		}

		public void Dispose()
		{
			_testServer.Dispose();
		}

		#region Test Handler

		[Fact]
		public async Task Handler_Post_Happy_Path_Should_Return_Ok()
		{
			// Arrange
			_request = new PostTopSecretSplitBodyRequest()
			{
				Distance = MockData.Satellite1RequestOk.Distance,
				Message = MockData.Satellite1RequestOk.Message,
			};

			// Act
			var response = await _topSecretSplitClient.PostAsync(_request, MockData.Satellite1RequestOk.Name);

			// Assert
			var result = $"The data for the satellite '{MockData.Satellite1RequestOk.Name}' was created successed";
			Assert.NotNull(response.ResponseWrapper.Data);
			Assert.Equal(result, response.ResponseWrapper.Data);
			Assert.Equal(HttpStatusCode.Created, response.StatusCode);
		}

		[Fact]
		public async Task Handler_Message_Have_Null_Element_Should_Return_Error()
		{
			// Arrange
			_request = new PostTopSecretSplitBodyRequest()
			{
				Distance = MockData.Satellite1RequestOk.Distance,
				Message = MockData.MessageCwithNull,
			};

			// Act
			var response = await _topSecretSplitClient.PostAsync(_request, MockData.Satellite1RequestOk.Name);

			// Assert
			Assert.True(response.ResponseWrapper.Errors.Contains(MessageErrors.TOPSECRET_MESSAGE_CONTAIN_NULL_ELEMENT));
			Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
		}

		[Fact]
		public async Task Handler_Satellite_Not_Found_Should_Return_Error()
		{
			// Arrange
			_request = new PostTopSecretSplitBodyRequest()
			{
				Distance = MockData.Satellite1RequestOk.Distance,
				Message = MockData.Satellite1RequestOk.Message,
			};
			var name = "abcde";

			// Act
			var response = await _topSecretSplitClient.PostAsync(_request, name);

			// Assert
			Assert.True(response.ResponseWrapper.Errors.Contains(string.Format(MessageErrors.TOPSECRET_SATELLITE_NOTFOUND, name)));
			Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
		}

		#endregion Test Handler

		#region Test Validator

		// Este test case falla debido a que internamente no acepta un parametro vacio y un valor es requerido
		//[Fact]
		//public async Task Validator_Name_Is_Required_Should_Return_Error()
		//{
		//    // Arrange
		//    _request = new PostTopSecretSplitBodyRequest()
		//    {
		//        Distance = MockData.SatelliteRequestOk.Distance,
		//        Message = MockData.SatelliteRequestOk.Message,
		//    };

		// // Act var response = await _topSecretSplitClient.PostTopSecretSplitAsync(_request, EMPTY_STRING);

		//    // Assert
		//    var messageFormat = string.Format(MessageErrors.VALIDATION_FIELD_IS_REQUIRED, "Name");
		//    Assert.True(response.Response.Errors.Contains(messageFormat));
		//    Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
		//}

		[Fact]
		public async Task Validator_Name_Is_Too_Long_Should_Return_Error()
		{
			// Arrange
			_request = new PostTopSecretSplitBodyRequest()
			{
				Distance = MockData.Satellite1RequestOk.Distance,
				Message = MockData.Satellite1RequestOk.Message,
			};
			var nameValue = "Lorem ipsum dolor sit amet, consectetur adipiscing elit";

			// Act
			var response = await _topSecretSplitClient.PostAsync(_request, nameValue);

			// Assert
			var messageFormat = string.Format(MessageErrors.VALIDATION_MAX_LENGTH, "Name", Constants.MAX_LENGTH_SATELLITE_NAME);
			Assert.True(response.ResponseWrapper.Errors.Contains(messageFormat));
			Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
		}

		[Fact]
		public async Task Validator_Distance_Is_Not_Decimal_Should_Return_Error()
		{
			// Arrange
			_request = new PostTopSecretSplitBodyRequest()
			{
				Distance = "distance",
				Message = MockData.Satellite1RequestOk.Message,
			};

			// Act
			var response = await _topSecretSplitClient.PostAsync(_request, MockData.Satellite1RequestOk.Name);

			// Assert
			var messageFormat = string.Format(MessageErrors.VALIDATION_MUST_BE_DECIMAL_AND_GREATER_THAN_ZERO, "Distance");
			Assert.True(response.ResponseWrapper.Errors.Contains(messageFormat));
			Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
		}

		[Fact]
		public async Task Validator_Distance_Is_Required_Should_Return_Error()
		{
			// Arrange
			_request = new PostTopSecretSplitBodyRequest()
			{
				Distance = "",
				Message = MockData.Satellite1RequestOk.Message,
			};

			// Act
			var response = await _topSecretSplitClient.PostAsync(_request, MockData.Satellite1RequestOk.Name);

			// Assert
			var messageFormat = string.Format(MessageErrors.VALIDATION_FIELD_IS_REQUIRED, "Distance");
			Assert.True(response.ResponseWrapper.Errors.Contains(messageFormat));
			Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
		}

		[Fact]
		public async Task Validator_Message_Empty_Should_Return_Error()
		{
			// Arrange
			_request = new PostTopSecretSplitBodyRequest()
			{
				Distance = MockData.Satellite1RequestOk.Distance,
				Message = new List<string>(),
			};

			// Act
			var response = await _topSecretSplitClient.PostAsync(_request, MockData.Satellite1RequestOk.Name);

			// Assert
			var messageFormat = string.Format(MessageErrors.VALIDATION_FIELD_IS_REQUIRED, "Message");
			Assert.True(response.ResponseWrapper.Errors.Contains(messageFormat));
			Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
		}

		[Fact]
		public async Task Validator_Message_Null_Should_Return_Error()
		{
			// Arrange
			_request = new PostTopSecretSplitBodyRequest()
			{
				Distance = MockData.Satellite1RequestOk.Distance,
				Message = null,
			};

			// Act
			var response = await _topSecretSplitClient.PostAsync(_request, MockData.Satellite1RequestOk.Name);

			// Assert
			var messageFormat = string.Format(MessageErrors.VALIDATION_FIELD_IS_REQUIRED, "Message");
			Assert.True(response.ResponseWrapper.Errors.Contains(messageFormat));
			Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
		}

		[Fact]
		public async Task Validator_Message_Item_Is_Too_Long_Should_Return_Error()
		{
			// Arrange
			_request = new PostTopSecretSplitBodyRequest()
			{
				Distance = MockData.Satellite1RequestOk.Distance,
				Message = new List<string>()
				{
					"Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolor",
					EMPTY_STRING,
					"secret"
				}
			};

			// Act
			var response = await _topSecretSplitClient.PostAsync(_request, MockData.Satellite1RequestOk.Name);

			// Assert
			var messageFormat = string.Format(MessageErrors.VALIDATION_MAX_LENGTH, "Message Item", Constants.MAX_LENGTH_WORD_TEXT);
			Assert.True(response.ResponseWrapper.Errors.Contains(messageFormat));
			Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
		}

		#endregion Test Validator
	}
}
