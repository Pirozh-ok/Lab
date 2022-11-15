using BookAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookAPI.Controllers
{
    [Route("api/books/")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private static List<Book> _books = new List<Book>();

        [HttpPost]
        public IActionResult Create([FromBody] Book book)
        {
            if (book is null)
            {
                return BadRequest("Book is null");
            }

            if(_books.Any(x => x.Id == book.Id))
            {
                return BadRequest("There is already a book with this ID");
            }

            _books.Add(book);
            return StatusCode(201); 
        }

        [HttpGet] 
        public IActionResult GetAll()
        {
            return Ok(_books);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var book = _books.FirstOrDefault(x => x.Id == id);

            return book is null ? NotFound() : Ok(book);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var book = _books.FirstOrDefault(x => x.Id == id);

            if(book is null)
            {
                return NotFound(); 
            }

            _books.Remove(book);
            return StatusCode(204);
        }

        [HttpPut]
        public IActionResult Put([FromBody] Book book)
        {
            if (book is null)
            {
                return BadRequest("Book is null");
            }

            var bookToUpdate = _books.FirstOrDefault(x => x.Id == book.Id);

            if (bookToUpdate is null)
            {
                return BadRequest("There is no book with this ID");
            }

            bookToUpdate = book; 
            return StatusCode(201);
        }

    }
}
