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
    public class SearchController : ControllerBase
    {
        private readonly BookContext _context;

        public SearchController(BookContext context)
        {
            _context = context;
        }


        // GET: /Book
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> Search([FromQuery]string query)
        {
            var book = await _context.Books
                        .Include(b => b.Genre)
                        .Include(b => b.Author)
                        .AsNoTracking()
                        .Where(b => b.Title.Contains(query)
                                    || b.Description.Contains(query)
                                    || b.Author.Name.Contains(query)
                                    || b.Genre.Name.Contains(query))
                        .ToListAsync();

            if(book.Count == 0) return NotFound();

            return book;

        }
    }
}
