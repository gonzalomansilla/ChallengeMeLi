using FluentValidation.Results;

using System;
using System.Collections.Generic;

/*
	Recolecta los mensajes de error de FluentValidation
 */

namespace ChallengeMeLi.Application.Exceptions
{
	public class ValidationException : Exception
	{
		public ValidationException() : base("One or more validation errors occurred")
		{
			Errors = new List<string>();
		}

		public ValidationException(IList<ValidationFailure> failures) : this()
		{
			foreach (var failure in failures)
			{
				Errors.Add(failure.ErrorMessage);
			}
		}

		public List<string> Errors { get; }
	}
}
