using FluentValidation;
using MomsKitchen.DATA.DTO.ApplicationUsers;

namespace MomsKitchen.DATA.Validators
{
    public class UserRequestValidator :AbstractValidator<UserRequest>
    {
        public UserRequestValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Firstname is required.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Lastname is required");

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Username is required.");

            RuleFor(x => x.Password)
                .MinimumLength(8).WithMessage("Password is too short.")
                .Matches("(?=.*[a-z])").WithMessage("Missing small letter.")
                .Matches("(?=.*[A-Z])").WithMessage("Missing capital letter.")
                .Matches("(?=.*[0-9])").WithMessage("Missing numbers.")
                .Matches("(?=.*[^A-Za-z0-9])").WithMessage("Missing special character.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("PhoneNumber is required.");
        }
    }
}
