using FastEndpoints;
using FluentValidation;
using MomsKitchen.Constants;
using MomsKitchen.Contracts.Recipes.Requests;

namespace MomsKitchen.Validators.Recipes;

public class UpdateRecipeRequestValidator : Validator<UpdateRecipeRequest>
{
    public UpdateRecipeRequestValidator()
    {
        RuleFor<string>(c => c.Name)
            .NotEmpty()
            .WithMessage(ValidationMessages.Required);
        
        RuleFor<string>(c => c.Description)
            .NotEmpty()
            .WithMessage(ValidationMessages.Required);
        
        RuleFor<List<Guid>>(c => c.Categories)
            .NotEmpty()
            .WithMessage(ValidationMessages.Required);
    }
}