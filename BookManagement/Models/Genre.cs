using System.Collections.Generic;

namespace BookManagement.Models
{
    public class Genre
    {
        public int GenreID { get; set; }
        public string Name { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}