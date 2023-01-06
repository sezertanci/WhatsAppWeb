using Common.Constants;
using Common.Models.RequestModels.UserRequestModels;
using FluentValidation;

namespace Application.Commands.UserCommand.Create
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.PhoneNumber).NotNull().NotEmpty().WithMessage(ValidatorConstants.PhoneNumberNotEmpty).Must(x => x.Length == 10).WithMessage(ValidatorConstants.PhoneNumberLength);
            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage(ValidatorConstants.NameNotEmpty).MaximumLength(50).WithMessage(ValidatorConstants.NameLength);
            RuleFor(x => x.Password).NotNull().NotEmpty().WithMessage(ValidatorConstants.PasswordNotEmpty).Length(6, 50).WithMessage(ValidatorConstants.PasswordLength);
        }
    }
}
