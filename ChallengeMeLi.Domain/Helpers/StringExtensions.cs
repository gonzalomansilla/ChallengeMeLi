using System;

namespace ChallengeMeLi.Domain.Helpers
{
	public static class StringExtensions
	{
		public static bool IsDecimalGreaterZero(this string value)
		{
			if (!IsDecimal(value))
				return false;

			if (decimal.Parse(value) <= uint.MinValue)
				return false;

			return true;
		}

		public static bool IsDecimal(this string numb)
		{
			try
			{
				Convert.ToDecimal(numb);
				return true;
			}
			catch (FormatException)
			{
				return false;
			}
		}
	}
}
