using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ChallengeMeLi.Domain.Helpers;

using Xunit;

namespace ChallengeMeLi.Tests.Domain.Helpers
{
	public class HelpersTests
	{
		[Fact]
		public void RoundWithTwoDecimals_Should_Return_Ok()
		{
			// Arrange
			decimal number = 3.168m;

			// Act
			decimal result = ChallengeMeLi.Domain.Helpers.Helpers.RoundWithTwoDecimals(number);

			// Assert
			Assert.Equal(3.17m, result);
		}

		[Fact]
		public void CalculateSquared_Should_Return_True()
		{
			// Arrange
			decimal number = 2.5m;

			// Act
			decimal result = ChallengeMeLi.Domain.Helpers.Helpers.CalculateSquared(number);

			// Assert
			Assert.Equal(6.25m, result);
		}

		[Fact]
		public void UnionListsWithSameValue_Should_Return_True()
		{
			// Arrange
			var list1 = new List<string> { "Hello", "" }.ToArray();
			var list2 = new List<string> { "", "world" }.ToArray();

			// Act
			var result = ChallengeMeLi.Domain.Helpers.Helpers.UnionListsWithSameValue(list1, list2);

			// Assert
			var joinedWords = string.Join(" ", result);
			Assert.Equal("Hello world", joinedWords);
		}

		// GetNotEmpty
		[Fact]
		public void GetNotEmpty_Should_Return_True()
		{
			// Arrange
			var text1 = "";
			var text2 = "World";

			// Act
			var result = ChallengeMeLi.Domain.Helpers.Helpers.GetNotEmpty(text1, text2);

			// Assert
			Assert.Equal(text2, result);
		}
	}
}
