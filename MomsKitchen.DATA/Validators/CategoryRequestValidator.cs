using FluentValidation;
using MomsKitchen.DATA.DTO.Categories;

namespace MomsKitchen.DATA.Validators
{
    public class CategoryRequestValidator : AbstractValidator<CategoryRequest>
    {
        public CategoryRequestValidator()
        {
            RuleFor(x => x.Name)
                    .NotEmpty()
                    .WithMessage("Name is required.");

            RuleFor(x => x.Description)
                    .NotEmpty()
                    .WithMessage("Description is required");
        }

    }
}
