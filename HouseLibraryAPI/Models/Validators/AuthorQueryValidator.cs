using FluentValidation;
using HouseLibraryAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseLibrary.Models.Validators
{
    public class AuthorQueryValidator : AbstractValidator<LibraryQuery>
    {
        private string[] allowedSortByColumnNames =
           {nameof(Author.FirstName), nameof(Author.LastName)};
        public AuthorQueryValidator()
        {
            RuleFor(r => r.SortBy)
                .Must(value => string.IsNullOrEmpty(value) || allowedSortByColumnNames.Contains(value))
                .WithMessage($"Sort by is optional, or must be in [{string.Join(",", allowedSortByColumnNames)}]");
        }
    }
}
