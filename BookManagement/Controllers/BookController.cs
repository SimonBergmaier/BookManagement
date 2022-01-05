using BookManagement.Context;
using BookManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookManagement.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly BookContext _context;

        public BookController(BookContext context)
        {
            _context = context;
        }

        // GET: /Book
        /// <summary>
        /// Lists all Books with their Authors and Genre
        /// </summary>
        /// <returns>A List of all Books.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            return await _context.Books
                        .Include(b => b.Genre)
                        .Include(b => b.Author)
                        .AsNoTracking()
                        .ToListAsync();
        }

        // GET /Book/5
        /// <summary>
        /// Displays a Book based on its ID.
        /// </summary>
        /// <param name="id">The ID of the Book to look for.</param>
        /// <returns>The returned Book, if no Book was found an 404 Error is returned.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            return await _context.Books
                        .Include(b => b.Genre)
                        .Include(b => b.Author)
                        .AsNoTracking()
                        .FirstOrDefaultAsync(b => b.BookID == id);
        }

        // POST /Book
        /// <summary>
        /// Creates a new Book.
        /// </summary>
        /// <param name="book">The new Book to create.</param>
        /// <returns>A representation of the new Book.</returns>
        [HttpPost]
        public async Task<ActionResult<Book>> PostBook([FromBody] Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBook), new { id = book.BookID }, book);
        }

        // PUT /Book/5
        /// <summary>
        /// Updates a Book based on its ID.
        /// </summary>
        /// <param name="id">The ID of the Book to edit.</param>
        /// <param name="book">The edited parameters of the Book</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, [FromBody] Book book)
        {
            if(id != book.BookID)
            {
                return BadRequest();
            }

            _context.Entry(book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if(!BookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        // DELETE /Book/5
        /// <summary>
        /// Deletes a Book from the Database
        /// </summary>
        /// <param name="id">The ID of the Book to delete.</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if(book == null)
            {
                return NotFound();
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Checks if a Book with the given ID exists.
        /// </summary>
        /// <param name="id">The ID of the Book to check for.</param>
        /// <returns>True if the Book exists, False if not.</returns>
        private bool BookExists(int id)
        {
            return _context.Books.Any(b => b.BookID == id);
        }
    }
}
