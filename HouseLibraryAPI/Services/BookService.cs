using AutoMapper;
using HouseLibrary.Models;
using HouseLibraryAPI.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseLibraryAPI.Services
{
    public interface IBookService
    {
        void AddBookWithAuthors(CreateBookDto dto);
        bool DeleteBookById(int bookId);
        IEnumerable<BookDto> GetAllBooks(LibraryQuery query);
        BookDto GetBookById(int bookId);
        bool UpdateBookById(int bookId, UpdateBookDto book);
    }

    public class BookService : IBookService
    {
        private readonly HouseLibraryDbContext _context;
        private readonly IMapper mapper;

        public BookService(HouseLibraryDbContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }
        public void AddBookWithAuthors(CreateBookDto dto)
        {
            var _book = mapper.Map<Book>(dto);

            _context.Books.Add(_book);
            _context.SaveChanges();

            foreach (var id in dto.AuthorIds)
            {
                var _book_author = new Author_Book()
                {
                    BookId = _book.Id,
                    AuthorId = id
                };
                _context.Author_Books.Add(_book_author);
                _context.SaveChanges();
            }
        }
        public IEnumerable<BookDto> GetAllBooks(LibraryQuery query)
        { 
            var books = _context.Books.ToList();
            var bookDtos = mapper.Map<List<BookDto>>(books)
                .Where(r => query.SearchPhrase == null || (r.Name.ToLower().Contains(query.SearchPhrase.ToLower())));

            if (!string.IsNullOrEmpty(query.SortBy))
            {
                bookDtos = bookDtos.OrderBy(x => x.Name);
            }

            return bookDtos;
        }
        public BookDto GetBookById(int bookId)
        {
            var _bookWithAuthors = _context.Books.Where(n => n.Id == bookId).Select(book => new BookDto()
            {
                Name = book.Name,
                Description = book.Language,
                Type = book.Type,
                ReleaseDate = book.ReleaseDate,
                PlaceOfPublication = book.PlaceOfPublication,
                Publisher = book.Publisher,
                Borrowed = book.Borrowed,
                Language = book.Language,
                AuthorNames = book.Author_Books.Select(n => n.Author.LastName).ToList()
            }).FirstOrDefault();

            return _bookWithAuthors;
        }
        public bool UpdateBookById(int bookId, UpdateBookDto book)
        {
            var _book = _context.Books.FirstOrDefault(n => n.Id == bookId);
            if (_book != null)
            {
                _book.Name = book.Name;
                _book.Description = book.Language;
                _book.Type = book.Type;
                _book.ReleaseDate = book.ReleaseDate;
                _book.PlaceOfPublication = book.PlaceOfPublication;
                _book.Publisher = book.Publisher;
                _book.Borrowed = book.Borrowed;
                _book.Language = book.Language;

                _context.SaveChanges();
                return true;
            }

            return false;
        }
        public bool DeleteBookById(int bookId)
        {
            var _book = _context.Books.FirstOrDefault(n => n.Id == bookId);
            if (_book != null)
            {
                _context.Books.Remove(_book);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

    }
}
