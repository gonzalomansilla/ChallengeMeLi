using System;
using System.Collections.Generic;
using System.Globalization;

namespace ChallengeMeLi.Application.Exceptions
{
	public class ApiException : Exception

	{
		public ApiException() : base("One or more errors occurred")
		{
		}

		public ApiException(IList<string> failures) : this()
		{
			Errors = failures;
		}

		public IList<string> Errors { get; } = new List<string>();
	}
}
