using FluentValidation;
using MomsKitchen.DATA.DTO.Recipes;

namespace MomsKitchen.DATA.Validators
{
    public class RecipeRequestValidator : AbstractValidator<RecipeRequest>
    {
        public RecipeRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required.");

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Description is required");

            RuleFor(x => x.ComplexityLevelId)
                .NotEmpty()
                .WithMessage("Complexity Level is required");
        }
    }
}
