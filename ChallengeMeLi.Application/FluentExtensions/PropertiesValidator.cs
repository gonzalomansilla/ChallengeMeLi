using ChallengeMeLi.Domain.Helpers;
using ChallengeMeLi.Shared.MessageErrors;

using FluentValidation;

namespace ChallengeMeLi.Application.FluentExtensions
{
	public static class PropertiesValidator
	{
		// Numeric
		public static IRuleBuilderOptions<T, string> IsValidDecimalType<T>(this IRuleBuilder<T, string> ruleBuilder)
		{
			return ruleBuilder
				.Must(StringExtensions.IsDecimalGreaterZero)
				.WithMessage(string.Format(MessageErrors.VALIDATION_MUST_BE_DECIMAL_AND_GREATER_THAN_ZERO, "{PropertyName}"));
		}
	}
}
