using ChallengeMeLi.Domain.Helpers;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

using Xunit;

namespace ChallengeMeLi.Tests.Domain.Helpers
{
	public class StringExtensionsTests
	{
		public StringExtensionsTests()
		{
		}

		#region IsDecimalGreaterZero

		[Fact]
		public void IsDecimalGreaterZero_Should_Return_True()
		{
			// Arrange
			string number = "1";

			// Act
			bool result = StringExtensions.IsDecimalGreaterZero(number);

			// Assert
			Assert.True(result);
		}

		[Fact]
		public void IsDecimalGreaterZero_Should_Return_False()
		{
			// Arrange
			string number = "0";

			// Act
			bool result = StringExtensions.IsDecimalGreaterZero(number);

			// Assert
			Assert.False(result);
		}

		#endregion IsDecimalGreaterZero

		#region IsDecimal

		[Fact]
		public void IsDecimal_Should_Return_True()
		{
			// Arrange
			string number = "3.14";

			// Act
			bool result = StringExtensions.IsDecimal(number);

			// Assert
			Assert.True(result);
		}

		[Fact]
		public void IsDecimal_Should_Return_False()
		{
			// Arrange
			string number = "abc";

			// Act
			bool result = StringExtensions.IsDecimal(number);

			// Assert
			Assert.False(result);
		}

		#endregion IsDecimal
	}
}
