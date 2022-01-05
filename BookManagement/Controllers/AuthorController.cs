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
    public class AuthorController : ControllerBase
    {
        private readonly BookContext _context;

        public AuthorController(BookContext context)
        {
            _context = context;
        }

        // GET: /Author
        /// <summary>
        /// Lists all Authors and their Books with their Genre
        /// </summary>
        /// <returns>A List of all Authors</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Author>>> GetAuthors()
        {
            return await _context.Authors
                .Include(a => a.Books)
                    .ThenInclude(b => b.Genre)
                .AsNoTracking()
                .ToListAsync();
        }

        // GET: /Author/5
        /// <summary>
        /// Displays an Author based on its ID.
        /// </summary>
        /// <param name="id">The ID of the Author to look for.</param>
        /// <returns>The returned Author, if no Author was found an 404 Error is returned.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Author>> GetAuthor(int id)
        {
            var author = await _context.Authors
                .Include(a => a.Books)
                    .ThenInclude(b => b.Genre)
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.AuthorID == id);

            if (author == null)
            {
                return NotFound();
            }

            return author;
        }

        // PUT: /Author/5
        /// <summary>
        /// Updates an author based on its ID.
        /// </summary>
        /// <param name="id">The ID of the Author to edit.</param>
        /// <param name="author">The edited parameters of the Author</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthor(int id, Author author)
        {
            if (id != author.AuthorID)
            {
                return BadRequest();
            }

            _context.Entry(author).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthorExists(id))
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

        // POST: /Author
        /// <summary>
        /// Creates a new Author.
        /// </summary>
        /// <param name="author">The new Author to create.</param>
        /// <returns>A representation of the new Author.</returns>
        [HttpPost]
        public async Task<ActionResult<Author>> PostAuthor(Author author)
        {
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAuthor), new { id = author.AuthorID }, author);
        }

        // DELETE: /Author/5
        /// <summary>
        /// Deletes an Author from the Database
        /// </summary>
        /// <param name="id">The ID of the Author to delete.</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Checks if an Author with the given ID exists.
        /// </summary>
        /// <param name="id">The ID of the Author to check for.</param>
        /// <returns>True if the Author exists, False if not.</returns>
        private bool AuthorExists(int id)
        {
            return _context.Authors.Any(a => a.AuthorID == id);
        }
    }
}
