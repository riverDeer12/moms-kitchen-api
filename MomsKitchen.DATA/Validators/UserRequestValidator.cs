using FluentValidation;
using MomsKitchen.DATA.DTO.ApplicationUsers;

namespace MomsKitchen.DATA.Validators
{
    public class UserRequestValidator :AbstractValidator<UserRequest>
    {
        public UserRequestValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("Firstname is required.");

            RuleFor(x => x.LastName).NotEmpty()
                .WithMessage("Lastname is required");

            RuleFor(x => x.UserName)
                .NotEmpty()
                .WithMessage("Username is required.");

            RuleFor(x => x.Password)
                .Matches("(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[^A-Za-z0-9])(?=.{8,})")
                .WithMessage("Password is too weak.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .WithMessage("PhoneNumber is required.");
        }
    }
}
