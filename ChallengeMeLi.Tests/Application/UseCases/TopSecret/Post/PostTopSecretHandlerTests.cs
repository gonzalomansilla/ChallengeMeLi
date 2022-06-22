using ChallengeMeLi.Application.Repositories;
using ChallengeMeLi.Application.UseCases.V1.TopSecret.Post;
using ChallengeMeLi.Domain.Common;
using ChallengeMeLi.Shared.MessageErrors;
using ChallengeMeLi.Tests.Client;
using ChallengeMeLi.Tests.Common;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

using Moq;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Xunit;
using System.Net;
using ChallengeMeLi.Domain.Helpers;

namespace ChallengeMeLi.Tests.Application.UseCases.TopSecret.Post
{
	/*
	 * Test de integracion para la api TopSecret
	 * **/

	public class PostTopSecretHandlerTests : IDisposable
	{
		protected readonly TestServer _testServer;
		private const string EMPTY_STRING = "";
		private readonly TopSecretClient _topSecretClient;
		private PostTopSecretRequestGroup _request;

		public PostTopSecretHandlerTests()
		{
			var webBuilder = new WebHostBuilder()
				.UseStartup<StartupTest>();

			_testServer = new TestServer(webBuilder);

			_topSecretClient = new TopSecretClient(_testServer.CreateClient());
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
			_request = new PostTopSecretRequestGroup()
			{
				Satellites = MockData.SatellitesRequestOk
			};

			// Act
			var response = await _topSecretClient.PostTopSecretAsync(_request);

			// Assert
			Assert.NotNull(response.ResponseWrapper.Data);
			Assert.NotNull(response.ResponseWrapper.Data.Position);
			Assert.Equal(MockData.POS_X_RESULT, Helpers.RoundWithTwoDecimals(response.ResponseWrapper.Data.Position.X));
			Assert.Equal(MockData.POS_Y_RESULT, Helpers.RoundWithTwoDecimals(response.ResponseWrapper.Data.Position.Y));
			Assert.Equal(MockData.FINAL_MESSAGE, response.ResponseWrapper.Data.Message);
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
		}

		[Fact]
		public async Task Handler_Satellites_In_Different_Order_Of_Entry_Return_Ok()
		{
			// Arrange
			_request = new PostTopSecretRequestGroup()
			{
				Satellites = MockData.SatellitesInDifferentOrderRequestOk
			};

			// Act
			var response = await _topSecretClient.PostTopSecretAsync(_request);

			// Assert
			Assert.NotNull(response.ResponseWrapper.Data);
			Assert.NotNull(response.ResponseWrapper.Data.Position);
			Assert.Equal(MockData.POS_X_RESULT, Helpers.RoundWithTwoDecimals(response.ResponseWrapper.Data.Position.X));
			Assert.Equal(MockData.POS_Y_RESULT, Helpers.RoundWithTwoDecimals(response.ResponseWrapper.Data.Position.Y));
			Assert.Equal(MockData.FINAL_MESSAGE, response.ResponseWrapper.Data.Message);
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
		}

		[Fact]
		public async Task Handler_Message_Have_Diferent_Length_Should_Return_Error()
		{
			// Arrange
			_request = new PostTopSecretRequestGroup()
			{
				Satellites = new List<SatelliteRequest>()
				{
					MockData.Satellite1RequestOk,
					MockData.Satellite1RequestOk,
					new SatelliteRequest()
					{
						Name = MockData.NAME_SATELLITE1,
						Distance = MockData.DISTANCE_SATELLITE1_REQ.ToString(),
						Message = new List<string>() { "word" },
					}
				}
			};

			// Act
			var response = await _topSecretClient.PostTopSecretAsync(_request);

			// Assert
			Assert.True(response.ResponseWrapper.Errors.Contains(MessageErrors.TOPSECRET_MESSAGES_ARE_NOT_SAME_LENGTH));
			Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
		}

		[Fact]
		public async Task Handler_Message_Have_Null_Element_Should_Return_Error()
		{
			// Arrange
			_request = new PostTopSecretRequestGroup()
			{
				Satellites = new List<SatelliteRequest>()
				{
					MockData.Satellite1RequestOk,
					MockData.Satellite1RequestOk,
					new SatelliteRequest()
					{
						Name = MockData.NAME_SATELLITE1,
						Distance = MockData.DISTANCE_SATELLITE1_REQ.ToString(),
						Message = MockData.MessageCwithNull,
					}
				}
			};

			// Act
			var response = await _topSecretClient.PostTopSecretAsync(_request);

			// Assert
			Assert.True(response.ResponseWrapper.Errors.Contains(MessageErrors.TOPSECRET_MESSAGE_CONTAIN_NULL_ELEMENT));
			Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
		}

		[Fact]
		public async Task Handler_Only_One_Satellite_Defined_Should_Return_Error()
		{
			// Arrange
			_request = new PostTopSecretRequestGroup()
			{
				Satellites = new List<SatelliteRequest>()
				{
					MockData.Satellite1RequestOk
				}
			};

			// Act
			var response = await _topSecretClient.PostTopSecretAsync(_request);

			// Assert
			Assert.True(response.ResponseWrapper.Errors.Contains(MessageErrors.TOPSECRET_ONLY_THREE_SATELLITES));
			Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
		}

		[Fact]
		public async Task Handler_Satellite_Not_Found_Should_Return_Error()
		{
			// Arrange
			var name = "abcde";

			_request = new PostTopSecretRequestGroup()
			{
				Satellites = new List<SatelliteRequest>()
				{
					MockData.Satellite1RequestOk,
					MockData.Satellite1RequestOk,
					new SatelliteRequest
					{
						Name = name,
						Distance = "1",
						Message = MockData.Message3
					}
				}
			};

			// Act
			var response = await _topSecretClient.PostTopSecretAsync(_request);

			// Assert
			Assert.True(response.ResponseWrapper.Errors.Contains(string.Format(MessageErrors.TOPSECRET_SATELLITE_NOTFOUND, name)));
			Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
		}

		#endregion Test Handler

		#region Test Validator

		[Fact]
		public async Task Validator_Distance_Is_Not_Decimal_Should_Return_Error()
		{
			// Arrange
			_request = new PostTopSecretRequestGroup()
			{
				Satellites = new List<SatelliteRequest>()
				{
					new SatelliteRequest()
					{
						Name = MockData.NAME_SATELLITE1,
						Distance = "decimal",
						Message = MockData.Message1,
					}
				}
			};

			// Act
			var response = await _topSecretClient.PostTopSecretAsync(_request);

			// Assert
			var messageFormat = string.Format(MessageErrors.VALIDATION_MUST_BE_DECIMAL_AND_GREATER_THAN_ZERO, "Distance");
			Assert.True(response.ResponseWrapper.Errors.Contains(messageFormat));
			Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
		}

		[Fact]
		public async Task Validator_Distance_Is_Required_Should_Return_Error()
		{
			// Arrange
			_request = new PostTopSecretRequestGroup()
			{
				Satellites = new List<SatelliteRequest>()
				{
					new SatelliteRequest()
					{
						Name = MockData.NAME_SATELLITE1,
						Distance = EMPTY_STRING,
						Message = MockData.Message1,
					}
				}
			};

			// Act
			var response = await _topSecretClient.PostTopSecretAsync(_request);

			// Assert
			var messageFormat = string.Format(MessageErrors.VALIDATION_FIELD_IS_REQUIRED, "Distance");
			Assert.True(response.ResponseWrapper.Errors.Contains(messageFormat));
			Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
		}

		[Fact]
		public async Task Validator_Message_Empty_Should_Return_Error()
		{
			// Arrange
			_request = new PostTopSecretRequestGroup()
			{
				Satellites = new List<SatelliteRequest>()
				{
					new SatelliteRequest()
					{
						Name = MockData.NAME_SATELLITE1,
						Distance = MockData.DISTANCE_SATELLITE1_REQ.ToString(),
						Message = new List<string>(),
					}
				}
			};

			// Act
			var response = await _topSecretClient.PostTopSecretAsync(_request);

			// Assert
			var messageFormat = string.Format(MessageErrors.VALIDATION_FIELD_IS_REQUIRED, "Message");
			Assert.True(response.ResponseWrapper.Errors.Contains(messageFormat));
			Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
		}

		[Fact]
		public async Task Validator_Message_Null_Should_Return_Error()
		{
			// Arrange
			_request = new PostTopSecretRequestGroup()
			{
				Satellites = new List<SatelliteRequest>()
				{
					new SatelliteRequest()
					{
						Name = MockData.NAME_SATELLITE1,
						Distance = MockData.DISTANCE_SATELLITE1_REQ.ToString(),
						Message = null,
					}
				}
			};

			// Act
			var response = await _topSecretClient.PostTopSecretAsync(_request);

			// Assert
			var messageFormat = string.Format(MessageErrors.VALIDATION_FIELD_IS_REQUIRED, "Message");
			Assert.True(response.ResponseWrapper.Errors.Contains(messageFormat));
			Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
		}

		[Fact]
		public async Task Validator_Message_Item_Is_Too_Long_Should_Return_Error()
		{
			// Arrange
			_request = new PostTopSecretRequestGroup()
			{
				Satellites = new List<SatelliteRequest>()
				{
					new SatelliteRequest()
					{
						Name = MockData.NAME_SATELLITE1,
						Distance = MockData.DISTANCE_SATELLITE1_REQ.ToString(),
						Message = new List<string>()
						{
							"Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolor",
							EMPTY_STRING,
							"secret"
						},
					}
				}
			};

			// Act
			var response = await _topSecretClient.PostTopSecretAsync(_request);

			// Assert
			var messageFormat = string.Format(MessageErrors.VALIDATION_MAX_LENGTH, "Message Item", Constants.MAX_LENGTH_WORD_TEXT);
			Assert.True(response.ResponseWrapper.Errors.Contains(messageFormat));
			Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
		}

		[Fact]
		public async Task Validator_Name_Is_Required_Should_Return_Error()
		{
			// Arrange
			_request = new PostTopSecretRequestGroup()
			{
				Satellites = new List<SatelliteRequest>()
				{
					new SatelliteRequest()
					{
						Name = EMPTY_STRING,
						Distance = MockData.DISTANCE_SATELLITE1_REQ.ToString(),
						Message = MockData.Message1,
					}
				}
			};

			// Act
			var response = await _topSecretClient.PostTopSecretAsync(_request);

			// Assert
			var messageFormat = string.Format(MessageErrors.VALIDATION_FIELD_IS_REQUIRED, "Name");
			Assert.True(response.ResponseWrapper.Errors.Contains(messageFormat));
			Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
		}

		[Fact]
		public async Task Validator_Name_Is_Too_Long_Should_Return_Error()
		{
			// Arrange
			_request = new PostTopSecretRequestGroup()
			{
				Satellites = new List<SatelliteRequest>()
				{
					new SatelliteRequest()
					{
						Name = "Lorem ipsum dolor sit amet, consectetur adipiscing elit",
						Distance = MockData.DISTANCE_SATELLITE1_REQ.ToString(),
						Message = MockData.Message1,
					}
				}
			};

			// Act
			var response = await _topSecretClient.PostTopSecretAsync(_request);

			// Assert
			var messageFormat = string.Format(MessageErrors.VALIDATION_MAX_LENGTH, "Name", Constants.MAX_LENGTH_SATELLITE_NAME);
			Assert.True(response.ResponseWrapper.Errors.Contains(messageFormat));
			Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
		}

		#endregion Test Validator
	}
}
