using Common.Constants;
using Common.Models.RequestModels.UserFriendRequestModels;
using FluentValidation;

namespace Application.Commands.UserFriendCommand.Create
{
    public class CreateUserFriendCommandValidator : AbstractValidator<CreateUserFriendCommand>
    {
        public CreateUserFriendCommandValidator()
        {
            RuleFor(x => x.UserId).NotNull().NotEmpty().WithMessage(ValidatorConstants.UserIdNotEmpty);
            RuleFor(x => x.PhoneNumber).NotNull().NotEmpty().WithMessage(ValidatorConstants.PhoneNumberNotEmpty).Must(x => x.Length == 10).WithMessage(ValidatorConstants.PhoneNumberLength);
            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage(ValidatorConstants.NameNotEmpty).MaximumLength(50).WithMessage(ValidatorConstants.NameLength);
        }
    }
}
