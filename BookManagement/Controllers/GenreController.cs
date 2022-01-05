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
    public class GenreController : ControllerBase
    {
        private readonly BookContext _context;

        public GenreController(BookContext context)
        {
            _context = context;
        }

        // GET: api/Genre
        /// <summary>
        /// Lists all Genres with their corresponding Books and Authors
        /// </summary>
        /// <returns>A List of all Genres</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Genre>>> GetGenres()
        {
            return await _context.Genres
                        .Include(g => g.Books)
                            .ThenInclude(b => b.Author)
                        .AsNoTracking()
                        .ToListAsync();
        }

        // GET: api/Genre/5
        /// <summary>
        /// Lists a specific Genre based on its ID.
        /// </summary>
        /// <param name="id">The ID to look for.</param>
        /// <returns>The specific Genre.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Genre>> GetGenre(int id)
        {
            var genre = await _context.Genres
                            .Include(g => g.Books)
                                .ThenInclude(b => b.Author)
                            .AsNoTracking()
                            .FirstOrDefaultAsync(g => g.GenreID == id);

            if (genre == null)
            {
                return NotFound();
            }

            return genre;
        }

        // PUT: api/Genre/5
        /// <summary>
        /// Updates the Genre specified by its ID.
        /// </summary>
        /// <param name="id">The ID of the Genre to be updated.</param>
        /// <param name="genre">The new parameters of the Genre.</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGenre(int id, Genre genre)
        {
            if (id != genre.GenreID)
            {
                return BadRequest();
            }

            _context.Entry(genre).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GenreExists(id))
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

        // POST: api/Genre
        /// <summary>
        /// Creates a new Genre.
        /// </summary>
        /// <param name="genre">The new Genre to create.</param>
        /// <returns>A representation of the new Genre</returns>
        [HttpPost]
        public async Task<ActionResult<Genre>> PostGenre(Genre genre)
        {
            _context.Genres.Add(genre);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetGenre), new { id = genre.GenreID }, genre);
        }

        // DELETE: api/Genre/5
        /// <summary>
        /// Deletes a Genre from the Database
        /// </summary>
        /// <param name="id">The ID of the Genre to delete.</param>
        [HttpDelete("{id}")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenre(int id)
        {
            var genre = await _context.Genres.FindAsync(id);
            if (genre == null)
            {
                return NotFound();
            }

            _context.Genres.Remove(genre);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Checks if an Genre with the given ID exists.
        /// </summary>
        /// <param name="id">The ID of the Genre to check for.</param>
        /// <returns>True if the Genre exists, False if not.</returns>
        private bool GenreExists(int id)
        {
            return _context.Genres.Any(g => g.GenreID == id);
        }
    }
}
