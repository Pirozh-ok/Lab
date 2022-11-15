using BookAPI.Models;
using BookAPI.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookAPI.Controllers
{
    [Route("api/books/")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private static IStorage<Book> _books = new MemCache();

        [HttpPost]
        public IActionResult Create([FromBody] Book book)
        {
            var validationResult = book.Validate();

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Error);
            }

            _books.Add(book);

            return Ok($"Book has been added");
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_books.All);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            if (!_books.Has(id))
            {
                return NotFound("No such");
            }

            return Ok(_books[id]);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            if (!_books.Has(id))
            {
                return NotFound("No such");
            }

            var valueToRemove = _books[id];
            _books.RemoveAt(id);

            return Ok($"Book has been removed");
        }

        [HttpPut]
        public IActionResult Put(Guid id, [FromBody] Book book)
        {
            if (!_books.Has(id))
            {
                return NotFound("No such");
            }

            var validationResult = book.Validate();

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Error);
            }

            var previousValue = _books[id];
            _books[id] = book;

            return Ok($"Book has been updated");
        }

    }
}
