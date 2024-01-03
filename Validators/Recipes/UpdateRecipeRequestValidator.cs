using FastEndpoints;
using FluentValidation;
using MomsKitchen.Contracts.Recipes.Requests;

namespace MomsKitchen.Validators.Recipes;

public class UpdateRecipeRequestValidator : Validator<UpdateRecipeRequest>
{
    public UpdateRecipeRequestValidator()
    {
        RuleFor<string>(c => c.Name)
            .NotEmpty()
            .WithMessage("Recipe name is required.");
        
        RuleFor<string>(c => c.Description)
            .NotEmpty()
            .WithMessage("Recipe description is required.");
        
        RuleFor<List<Guid>>(c => c.Categories)
            .NotEmpty()
            .WithMessage("At least one category needs to be selected.");
    }
}