using HouseLibrary.Models;
using HouseLibraryAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseLibraryAPI.Controllers
{
    [Route("api/author")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private IAuthorService _authorsService;
        public AuthorController(IAuthorService authorsService)
        {
            _authorsService = authorsService;
        }

        [HttpPost]
        public IActionResult AddBook([FromBody] CreateAuthorDto author)
        {
            _authorsService.AddAuthor(author);
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetAuthorWithBooks([FromRoute] int id)
        {
            var response = _authorsService.GetAuthorWithBooks(id);
            if(response is null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpGet]
        public IActionResult GetAllBooks([FromQuery] LibraryQuery query)
        {
            var allAuthors = _authorsService.GetAllAuthors(query);
            return Ok(allAuthors);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBookById([FromRoute] int id, [FromBody] UpdateAuthorDto dto)
        {
            var updatedAuthor = _authorsService.UpdateAuthorById(id, dto);
            if (!updatedAuthor)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAuthorById([FromRoute] int id)
        {
            var deleted = _authorsService.DeleteAuthorById(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }

    }
}
