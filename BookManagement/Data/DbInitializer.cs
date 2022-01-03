using BookManagement.Context;
using BookManagement.Models;
using System;
using System.Linq;

namespace BookManagement.Data
{
    public class DbInitializer
    {
        public static void Initialize(BookContext context)
        {
            context.Database.EnsureCreated();

            // Look for any books.
            if (context.Books.Any())
            {
                return;  // DB has been seeded
            }

            var genres = new Genre[]
            {
                new Genre { Name = "Thriller" },
                new Genre { Name = "Fantasy" },
                new Genre { Name = "Non-Fiction"}
            };

            foreach(Genre gen in genres)
            {
                context.Genres.Add(gen);
            }
            context.SaveChanges();

            var authors = new Author[]
            {
                new Author { Name = "Dan Brown", Country = "USA", Biography = "Daniel Gerhard Brown (born June 22, 1964) is an American author best known for his thriller novels, including the Robert Langdon novels." },
                new Author { Name = "J.K. Rowling", Country = "UK", Biography = "Joanne Rowling (born 31 July, 1965), better known by her pen name J.K. Rowling, is a british author, philanthropist, film producer, and screenwriter, She is author of the Harry Potter fantasy series."},
                new Author { Name = "Sun Tzu", Country = "China", Biography = "Sun Tzu was a Chinese general, military strategist, writer, and philosopher who lived in the Easter Zhou period of ancient China. Sun Tzu is traditionally credited as the author of The Art of War, an influental work of military strategy that has affected both Western and East Asian philosophy and military thinking."}
            };

            foreach(Author author in authors)
            {
                context.Authors.Add(author);
            }

            context.SaveChanges();

            var books = new Book[]
            {
                new Book { Title = "The Da Vinci Code", Description = "The Da Vinci Code follows symbologist Robert Langdon and cryptologist Sophie Neveu after a murder in the Louvre Museum in Paris causes them to become involved in a battle between the Priory of Sion and Opus Dei over the possibility of Jesus Christ and Mary Magdalene having had a child together.", Year = 2003, AuthorID = 1, GenreID = 1 },
                new Book { Title = "Harry Potter and the Deathly Hallows", Description = "The novel chronicles the events directly following Harry Potter and the Half-Blood Prince and the final confrontation between the wizards Harry Potter and Lord Voldemort.", Year = 2007, AuthorID = 2, GenreID = 2 },
                new Book { Title = "Harry Potter and the Half-Blood Prince", Description = "Set during Harry Potter's sixth year at Hogwarts, the novel explores the past of the boy wizard's nemesis, Lord Voldemort, and Harry's preparations for the final battle against Voldemort alongside his headmaster and mentor Albus Dumbledore.", Year = 2005, AuthorID = 2, GenreID = 2 },
                new Book { Title = "The Art of War", Description = "The book contains a detailed explanation and analysis of the 5th-century Chinese military, from weapons and strategy to rank and discipline. Sun also stressed the importance of intelligence operatives and espionage to the war effort. Considered one of history's finest military tacticians and analysts, his teachings and strategies formed the basis of advanced military training for millennia to come.", Year = 500, AuthorID = 3, GenreID = 3 },

            };

            foreach (Book b in books)
            {
                context.Books.Add(b);
            }
            context.SaveChanges();
        }

    }
}
