using ChallengeMeLi.Application.FuegoDeQuasar;
using ChallengeMeLi.Domain.Helpers;
using ChallengeMeLi.Tests.Common;

using System.Collections.Generic;
using System.Linq;

using Xunit;

namespace ChallengeMeLi.Tests.Application.FuegoDeQuasar
{
	public class FuegoDeQuasarTests
	{
		private readonly IList<string> messageA;
		private readonly IList<string> messageB;
		private readonly IList<string> messageC;
		private MainFuegoDeQuasar _fuegoDeQuasar;

		public FuegoDeQuasarTests()
		{
			messageA = MockData.Message1;
			messageB = MockData.Message2;
			messageC = MockData.Message3;
		}

		[Fact]
		public void GetLocations_Happy_Path_Should_Return_OK()
		{
			// Arrange
			_fuegoDeQuasar = new MainFuegoDeQuasar(MockData.SatellitesDB);

			// Setup
			var result = _fuegoDeQuasar.GetLocations(
				MockData.DISTANCE_SATELLITE1_REQ,
				MockData.DISTANCE_SATELLITE2_REQ,
				MockData.DISTANCE_SATELLITE3_REQ);

			// Assert
			Assert.Equal(MockData.POS_X_RESULT, Helpers.RoundWithTwoDecimals(result.X));
			Assert.Equal(MockData.POS_Y_RESULT, Helpers.RoundWithTwoDecimals(result.Y));
		}

		[Fact]
		public void GetMessage_Happy_Path_Should_Return_OK()
		{
			// Arrange
			_fuegoDeQuasar = new MainFuegoDeQuasar();

			// Setup
			var result = _fuegoDeQuasar.GetMessage(
				messageA.ToArray(),
				messageB.ToArray(),
				messageC.ToArray());

			// Assert
			Assert.Equal(MockData.FINAL_MESSAGE, result);
		}

		[Fact]
		public void GetMessage_Messages_In_Different_Order_Should_Return_OK()
		{
			// Arrange
			_fuegoDeQuasar = new MainFuegoDeQuasar();

			// Setup
			var result = _fuegoDeQuasar.GetMessage(
				messageC.ToArray(),
				messageA.ToArray(),
				messageB.ToArray());

			// Assert
			Assert.Equal(MockData.FINAL_MESSAGE, result);
		}
	}
}
