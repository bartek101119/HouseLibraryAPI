using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseLibrary.Models.Validators
{
    public class UpdateAuthorDtoValidator : AbstractValidator<UpdateAuthorDto>
    {
        public UpdateAuthorDtoValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .MaximumLength(50);
            RuleFor(x => x.LastName)
                .NotEmpty()
                .MaximumLength(50);
            RuleFor(x => x.PlaceOfBirth)
                .MaximumLength(50);
            RuleFor(x => x.Nationality)
                .MaximumLength(50);
        }
    }
}
