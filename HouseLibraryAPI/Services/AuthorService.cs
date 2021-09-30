using AutoMapper;
using HouseLibrary.Models;
using HouseLibraryAPI.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HouseLibraryAPI.Services
{
    public interface IAuthorService
    {
        void AddAuthor(CreateAuthorDto dto);
        IEnumerable<AuthorDto> GetAllAuthors(LibraryQuery query);
        AuthorDto GetAuthorWithBooks(int authorId);
        bool UpdateAuthorById(int authorId, UpdateAuthorDto dto);
        bool DeleteAuthorById(int authorId);
    }

    public class AuthorService : IAuthorService
    {
        private readonly HouseLibraryDbContext _context;
        private readonly IMapper _mapper;

        public AuthorService(HouseLibraryDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public void AddAuthor(CreateAuthorDto dto)
        {
            var _author = _mapper.Map<Author>(dto);

            _context.Authors.Add(_author);
            _context.SaveChanges();
        }
        public AuthorDto GetAuthorWithBooks(int authorId)
        {
            var _author = _context.Authors.Where(n => n.Id == authorId).Select(n => new AuthorDto()
            {
                FirstName = n.FirstName,
                LastName = n.LastName,
                Nationality = n.Nationality,
                PlaceOfBirth = n.PlaceOfBirth,
                DateOfBirth = n.DateOfBirth,
                BookTitles = n.Author_Books.Select(n => n.Book.Name).ToList()
            }).FirstOrDefault();

            return _author;
        }

        public IEnumerable<AuthorDto> GetAllAuthors(LibraryQuery query)
        {
            var authors = _context.Authors
                .Where(r => query.SearchPhrase == null || (r.FirstName.ToLower().Contains(query.SearchPhrase.ToLower())
                                                           || r.LastName.ToLower()
                                                               .Contains(query.SearchPhrase.ToLower())));

            if (!string.IsNullOrEmpty(query.SortBy))
            {
                var columnsSelectors = new Dictionary<string, Expression<Func<Author, object>>>
                {
                    { nameof(Author.FirstName), r => r.FirstName },
                    { nameof(Author.LastName), r => r.LastName },
                };

                var selectedColumn = columnsSelectors[query.SortBy];

                authors = query.SortDirection == SortDirection.ASC
                    ? authors.OrderBy(selectedColumn)
                    : authors.OrderByDescending(selectedColumn);
            }
            var allAuthorDtos = _mapper.Map<List<AuthorDto>>(authors);

            return allAuthorDtos;
        }

        public bool UpdateAuthorById(int authorId, UpdateAuthorDto dto)
        {
            var _author = _context.Authors.FirstOrDefault(n => n.Id == authorId);
            if (_author != null)
            {
                _author.FirstName = dto.FirstName;
                _author.LastName = dto.LastName;
                _author.Nationality = dto.Nationality;
                _author.PlaceOfBirth = dto.PlaceOfBirth;
                _author.DateOfBirth = dto.DateOfBirth;

                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool DeleteAuthorById(int authorId)
        {
            var _author = _context.Authors.FirstOrDefault(n => n.Id == authorId);
            if (_author != null)
            {
                _context.Authors.Remove(_author);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
