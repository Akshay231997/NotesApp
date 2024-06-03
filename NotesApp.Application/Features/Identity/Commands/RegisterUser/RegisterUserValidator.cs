using FluentValidation;
using NotesApp.Application.Contracts.Persistence;

namespace NotesApp.Application.Features.Identity.Commands.RegisterUser;

public class RegisterUserValidator : AbstractValidator<RegisterUserCommand>
{
    private readonly ICommonRepository _commonRepository;

    public RegisterUserValidator(ICommonRepository commonRepository)
    {
        _commonRepository = commonRepository;

        RuleFor(x => x)
            .NotNull().WithMessage("{PropertyName} can not be null")
            .DependentRules(() =>
            {
                RuleFor(x => x.FirstName)
                    .NotEmpty().WithMessage("{PropertyName} can not be empty or whitespace.");

                RuleFor(x => x.LastName)
                    .NotEmpty().WithMessage("{PropertyName} can not be empty or whitespace.");

                RuleFor(x => x.EmailId)
                    .EmailAddress().WithMessage("Invalid {PropertyName}.")
                    .DependentRules(() =>
                    {
                        RuleFor(x => x.EmailId)
                        .MustAsync(async (emailId, cancellationToken) => !await EmailIdExists(emailId))
                        .WithMessage("{PropertyName} already exists.");
                    });

                RuleFor(x => x.Password)
                    .NotEmpty().WithMessage("{PropertyName} cannot be empty")
                    .MinimumLength(8).WithMessage("{PropertyName} length must be at least 8.")
                    .MaximumLength(16).WithMessage("{PropertyName} length must not exceed 16.")
                    .Matches(@"[A-Z]+").WithMessage("{PropertyName} must contain at least one uppercase letter.")
                    .Matches(@"[a-z]+").WithMessage("{PropertyName} must contain at least one lowercase letter.")
                    .Matches(@"[0-9]+").WithMessage("{PropertyName} must contain at least one number.")
                    .Matches(@"[\!\@\#\$\%\^\&\*]+").WithMessage("{PropertyName} must contain at least one special character.");
            });
    }

    private async Task<bool> EmailIdExists(string emailId)
    {
        bool emailIdExists = false;

        if (!string.IsNullOrEmpty(emailId) && !string.IsNullOrWhiteSpace(emailId))
        {
            emailIdExists = await _commonRepository.EmailIdExistsAsync(emailId);
        }

        return emailIdExists;
    }
}