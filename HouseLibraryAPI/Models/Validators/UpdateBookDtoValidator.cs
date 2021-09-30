using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseLibrary.Models.Validators
{
    public class UpdateBookDtoValidator : AbstractValidator<UpdateBookDto>
    {
        public UpdateBookDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100);
            RuleFor(x => x.Description)
                .MaximumLength(3000);
            RuleFor(x => x.Type)
                .MaximumLength(50);
            RuleFor(x => x.PlaceOfPublication)
                .MaximumLength(50);
            RuleFor(x => x.Publisher)
                .MaximumLength(50);
        }
    }
}
