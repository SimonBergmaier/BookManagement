using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using BookManagement.Models;
using BookManagement.Context;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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

        // GET: api/<ValuesController>
        [HttpGet]
        public async Task<IEnumerable<Book>> Get()
        {
            return await _context.Books
                .Include(b => b.Genre)
                .Include(b => b.Author)
                .AsNoTracking()
                .ToListAsync();
        } 

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public async Task<Book> Get(int id)
        {
            return await _context.Books
                .Include(b => b.Genre)
                .Include(b => b.Author)
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.BookID == id);
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] Book value)
        {
            _context.Books.Add(value);
            _context.SaveChanges();
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Book value)
        {
            _context.Books.Update(value);
            _context.SaveChanges();
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var book = _context.Books.Find(id);
            _context.Books.Remove(book);
            _context.SaveChanges();
        }
    }
}
