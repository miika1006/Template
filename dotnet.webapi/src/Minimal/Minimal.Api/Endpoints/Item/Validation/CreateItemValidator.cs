using System;
using FluentValidation;
using Minimal.Api.Endpoints.Item.Models;

namespace Minimal.Api.Endpoints.Item.Validation
{
    public class CreateItemValidator : AbstractValidator<CreateItemRequest>
    {
        public CreateItemValidator()
        {
            _ = this.RuleFor(r => r.Name).NotNull().NotEmpty().WithMessage("Name is required, it cannot be null or empty.");
        }
    }
}

