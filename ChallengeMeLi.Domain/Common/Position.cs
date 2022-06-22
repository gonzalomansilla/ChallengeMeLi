using System.Diagnostics.CodeAnalysis;

namespace ChallengeMeLi.Domain.Common
{
	[ExcludeFromCodeCoverage]
	public class Position
	{
		public Position(decimal x, decimal y)
		{
			X = x;
			Y = y;
		}

		public decimal X { get; set; }
		public decimal Y { get; set; }
	}
}
