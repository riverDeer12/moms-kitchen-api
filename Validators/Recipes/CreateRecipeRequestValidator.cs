using FastEndpoints;
using FluentValidation;
using MomsKitchen.Contracts.Recipes.Requests;

namespace MomsKitchen.Validators.Recipes;

public class CreateRecipeRequestValidator : Validator<CreateRecipeRequest>
{
    public CreateRecipeRequestValidator()
    {
        RuleFor<string>(c => c.Name)
            .NotEmpty()
            .WithMessage("Recipe name is required.");
        
        RuleFor<string>(c => c.Description)
            .NotEmpty()
            .WithMessage("Recipe description is required.");
    }
}