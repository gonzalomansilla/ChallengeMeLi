using ChallengeMeLi.Application.FluentExtensions;
using ChallengeMeLi.Domain.Common;
using ChallengeMeLi.Shared.MessageErrors;

using FluentValidation;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeMeLi.Application.UseCases.V1.TopSecretSplit.Post
{
    public class PostTopSecretSplitValidator : AbstractValidator<PostTopSecretSplitRequest>
    {
        public PostTopSecretSplitValidator()
        {
            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage(string.Format(MessageErrors.VALIDATION_FIELD_IS_REQUIRED, "{PropertyName}"))
                .MaximumLength(Constants.MAX_LENGTH_SATELLITE_NAME)
                .WithMessage(string.Format(MessageErrors.VALIDATION_MAX_LENGTH, "{PropertyName}", Constants.MAX_LENGTH_SATELLITE_NAME));

            RuleFor(x => x.Distance)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage(string.Format(MessageErrors.VALIDATION_FIELD_IS_REQUIRED, "{PropertyName}"))
                .IsValidDecimalType();

            RuleFor(x => x.Message)
                .Cascade(CascadeMode.Stop)
                .Must(x => x != null && x.Any())
                .WithMessage(string.Format(MessageErrors.VALIDATION_FIELD_IS_REQUIRED, "{PropertyName}"));

            RuleForEach(x => x.Message)
                .ChildRules(itemM =>
                {
                    itemM.RuleFor(x => x)
                        .MaximumLength(Constants.MAX_LENGTH_WORD_TEXT)
                        .WithMessage(string.Format(MessageErrors.VALIDATION_MAX_LENGTH, "Message Item", Constants.MAX_LENGTH_WORD_TEXT));
                });
        }
    }
}