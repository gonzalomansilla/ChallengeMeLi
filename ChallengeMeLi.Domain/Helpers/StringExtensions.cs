using System;
using System.Text.RegularExpressions;

namespace ChallengeMeLi.Domain.Helpers
{
	public static class StringExtensions
	{
		public static bool IsIntGreaterZero(this string numb)
		{
			try
			{
				if (int.TryParse(numb, out int result))
					return result > 0;

				return false;
			}
			catch (FormatException)
			{
				return false;
			}
			catch (OverflowException)
			{
				return false;
			}
		}

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
			catch (OverflowException)
			{
				return false;
			}
		}
	}
}
