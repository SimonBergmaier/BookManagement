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
        [HttpPost]
        public async Task<ActionResult<Book>> PostBook([FromBody] Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBook), new { id = book.BookID }, book);
        }

        // PUT /Book/5
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

        private bool BookExists(int id)
        {
            return _context.Books.Any(b => b.BookID == id);
        }
    }
}
