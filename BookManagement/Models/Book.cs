namespace BookManagement.Models
{
    public class Book
    {
        public int BookID { get; set; }
        public int GenreID { get; set; }
        public int AuthorID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Year { get; set; }
        public Genre Genre { get; set; }
        public Author Author { get; set; }

    }
}
