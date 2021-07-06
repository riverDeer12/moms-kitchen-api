using FluentValidation;
using MomsKitchen.DATA.DTO.Recipes;

namespace MomsKitchen.DATA.Validators
{
    public class RecipeRequestValidator : AbstractValidator<RecipeRequest>
    {
        public RecipeRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty()
                .WithMessage("Name is required.");

            RuleFor(x => x.Description).NotNull()
                .WithMessage("Description is required");
        }
    }
}
