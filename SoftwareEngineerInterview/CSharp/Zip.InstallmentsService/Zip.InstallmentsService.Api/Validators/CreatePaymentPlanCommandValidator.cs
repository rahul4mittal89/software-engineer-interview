using FluentValidation;
using Zip.InstallmentsService.Api.Common.Commands;
using Zip.InstallmentsService.Api.Common.Extensions;

namespace Zip.InstallmentsService.Api.Validators;

/// <summary>
/// Command for Create Payment Plan
/// </summary>
public class CreatePaymentPlanCommandValidator : AbstractValidator<CreatePaymentPlanCommand>
{
    public CreatePaymentPlanCommandValidator()
    {
        RuleFor(c => c.PurchaseAmount).Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(c => $"'{nameof(c.PurchaseAmount)}' is required.")
            .GreaterThanOrEqualTo(1)
            .WithMessage(c => $"'{nameof(c.PurchaseAmount)}' must be greater than 1.")
            .PrecisionScale(9, 3, true)
            .WithMessage(c =>
                $"'{nameof(c.PurchaseAmount)}' must not be more contain more than '9' digits in total, with a maximum '3' decimal places.");

        RuleFor(c => c.Frequency).Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(c => $"'{nameof(c.Frequency)}' is required.")
            .GreaterThanOrEqualTo(1)
            .WithMessage(c => $"'{nameof(c.Frequency)}' must be greater than 1.");

        RuleFor(c => c.NumberOfInstallments).Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(c => $"'{nameof(c.NumberOfInstallments)}' is required.")
            .GreaterThanOrEqualTo(1)
            .WithMessage(c => $"'{nameof(c.NumberOfInstallments)}' must be greater than 1.");

        RuleFor(c => c.PurchaseDate).Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(c => $"'{nameof(c.PurchaseDate)}' is required.")
            .Must(date => date.TryParseIso8601DateTimeToUtc(out _))
            .WithMessage(c => $"'{c.PurchaseDate}' is not valid ISO 8601 date.")
            .Must(CheckDateNotInPast)
            .WithMessage(c => $"'{nameof(c.PurchaseDate)}' cannot be in the past.");
    }

    private static bool CheckDateNotInPast(string isoDate)
    {
        isoDate.TryParseIso8601DateTimeToUtc(out var date);

        return date.Date >= DateTime.UtcNow.Date;
    }
}