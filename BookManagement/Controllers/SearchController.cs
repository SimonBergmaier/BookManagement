using BookManagement.Context;
using BookManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

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
            return await _context.Books
                        .Include(b => b.Genre)
                        .Include(b => b.Author)
                        .AsNoTracking()
                        .Where(b =>    b.Title.Contains(query)
                                    || b.Description.Contains(query)
                                    || b.Author.Name.Contains(query)
                                    || b.Genre.Name.Contains(query))
                        .ToListAsync();

        }
    }
}
