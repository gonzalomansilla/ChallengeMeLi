using ChallengeMeLi.Application.FluentExtensions;
using ChallengeMeLi.Domain.Common;
using ChallengeMeLi.Shared.MessageErrors;

using FluentValidation;

using System.Linq;

namespace ChallengeMeLi.Application.UseCases.V1.TopSecret.Post
{
    /*
		Validaciones del request para el endpoint /topsecret
	 */

    public class PostTopSecretValidator : AbstractValidator<PostTopSecretRequestGroup>
    {
        public PostTopSecretValidator()
        {
            RuleForEach(x => x.Satellites)
                .ChildRules(item =>
                {
                    item.RuleFor(x => x.Name)
                        .Cascade(CascadeMode.Stop)
                        .NotEmpty()
                        .WithMessage(string.Format(MessageErrors.VALIDATION_FIELD_IS_REQUIRED, "{PropertyName}"))
                        .MaximumLength(Constants.MAX_LENGTH_SATELLITE_NAME)
                        .WithMessage(string.Format(MessageErrors.VALIDATION_MAX_LENGTH, "{PropertyName}", Constants.MAX_LENGTH_SATELLITE_NAME));

                    item.RuleFor(x => x.Distance)
                        .Cascade(CascadeMode.Stop)
                        .NotEmpty()
                        .WithMessage(string.Format(MessageErrors.VALIDATION_FIELD_IS_REQUIRED, "{PropertyName}"))
                        .IsValidDecimalType();

                    item.RuleFor(x => x.Message)
                        .Cascade(CascadeMode.Stop)
                        .Must(x => x != null && x.Any())
                        .WithMessage(string.Format(MessageErrors.VALIDATION_FIELD_IS_REQUIRED, "{PropertyName}"));

                    item.RuleForEach(x => x.Message)
                        .ChildRules(itemM =>
                        {
                            itemM.RuleFor(x => x)
                                .MaximumLength(Constants.MAX_LENGTH_WORD_TEXT)
                                .WithMessage(string.Format(MessageErrors.VALIDATION_MAX_LENGTH, "Message Item", Constants.MAX_LENGTH_WORD_TEXT));
                        });
                });
        }
    }
}