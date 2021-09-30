using HouseLibrary.Models;
using HouseLibraryAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseLibraryAPI.Controllers
{
    [Route("api/book")]
    [ApiController]
    public class BookController : ControllerBase
    {
        public IBookService _booksService;
        public BookController(IBookService booksService)
        {
            _booksService = booksService;
        }
        [HttpGet]
        public IActionResult GetAllBooks([FromQuery] LibraryQuery query)
        {
            var allBooks = _booksService.GetAllBooks(query);
            return Ok(allBooks);
        }

        [HttpGet("{id}")]
        public IActionResult GetBookById([FromRoute] int id)
        {
            var book = _booksService.GetBookById(id);
            if(book is null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookDto book)
        {
            _booksService.AddBookWithAuthors(book);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBookById([FromRoute] int id, [FromBody] UpdateBookDto book)
        {
            var updatedBook = _booksService.UpdateBookById(id, book);
            if (!updatedBook)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBookById([FromRoute] int id)
        {
            var deleted = _booksService.DeleteBookById(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
