using ConsoleTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UHRRJ1_HFT_2022232.Models;

namespace UHRRJ1_HFT_2022232.Client
{
    public class AuthorsBookCount
    {
        public string Name { get; internal set; }
        public int BookCount { get; internal set; }

        public AuthorsBookCount(string name, int bookCount)
        {
            Name = name;
            BookCount = bookCount;
        }

        public AuthorsBookCount()
        {

        }

        public override bool Equals(object obj)
        {
            AuthorsBookCount other = obj as AuthorsBookCount;
            return Name.Equals(other.Name)
                && BookCount == other.BookCount;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, BookCount);
        }
    }

    internal class Program
    {
        static RestService rest;

        static void Create(string entity)
        {
            Console.WriteLine("\n"+entity + " create");

            if (entity == "Reader")
            {
                Console.Write("Name: ");
                string name = Console.ReadLine();
                rest.Post(new Reader() { ReaderName = name}, "Reader");
            }
            if (entity == "BookStore")
            {
                Console.Write("BookId: ");
                string BookId = Console.ReadLine();

                Console.Write("ReaderId: ");
                string ReaderId = Console.ReadLine();

                Console.Write("BookStoreName: ");
                string BookStoreName = Console.ReadLine();

                rest.Post(new BookStore() 
                { 
                    BookStoreName = BookStoreName,
                    BookId = int.Parse(BookId),
                    ReaderId = int.Parse(ReaderId),
                }, "BookStore");
            }
            if (entity == "Author")
            {
                Console.Write("Name: ");
                string name = Console.ReadLine();
                rest.Post(new Author() { AuthorName = name }, "Author");
            }
            if (entity == "Book")
            {
                Console.Write("Title: ");
                string Title = Console.ReadLine();

                Console.Write("Price: ");
                string Income = Console.ReadLine();

                Console.Write("AuthorId: ");
                string AuthorId = Console.ReadLine();

                Console.Write("Release: ");
                string Release = Console.ReadLine();

                Console.Write("Rating: ");
                string Rating = Console.ReadLine();

                rest.Post(new Book()
                { 
                    Title = Title,
                    Price = int.Parse(Income),
                    AuthorId = int.Parse(AuthorId),
                    Release = DateTime.Parse(Release),
                    Rating = int.Parse(Rating)

                }, "Book");
            }

            Console.WriteLine(entity + " created and added.");
            Console.ReadLine();
        }
        static void List(string entity)
        {
            if (entity == "Reader")
            {
                Console.WriteLine("Id" + "\t" + "Name");
                List<Reader> Readers = rest.Get<Reader>("Reader");
                foreach (var item in Readers)
                {
                    Console.WriteLine(item.ReaderId + "\t" + item.ReaderName);
                }
            }
            if (entity == "BookStore")
            {
                Console.WriteLine("id" + "\t" + "Name");
                List<BookStore> Libraries = rest.Get<BookStore>("BookStore");
                foreach (var item in Libraries)
                {
                    Console.WriteLine(item.BookStoreId + "\t" + item.BookStoreName);
                }
            }
            if (entity == "Author")
            {
                Console.WriteLine("Id" + "\t" + "Name");
                List<Author> Authors = rest.Get<Author>("Author");
                foreach (var item in Authors)
                {
                    Console.WriteLine(item.AuthorId + "\t" + item.AuthorName);
                }


            }
            if (entity == "Book")
            {
                Console.WriteLine("Id" + "\t" + "Title");
                List<Book> Books = rest.Get<Book>("Book");
                foreach (var item in Books)
                {
                    Console.WriteLine(item.BookId + "\t" + item.Title);
                }
            }
            Console.ReadLine();
        }
        static void Update(string entity)
        {
            Console.WriteLine("\n"+entity + " update");

            if (entity == "Reader")
            {
                Console.Write("Id: ");
                int id = int.Parse(Console.ReadLine());

                Console.Write("new Name: ");
                string name = Console.ReadLine();

                Reader a = rest.Get<Reader>(id, "Reader");
                a.ReaderName = name;
                rest.Put(a,"Reader");
            }
            if (entity == "BookStore")
            {
                Console.Write("BookStoreId: ");
                string BookStoreId = Console.ReadLine();

                Console.Write("new BookId: ");
                string BookId = Console.ReadLine();

                Console.Write("new ReaderId: ");
                string ReaderId = Console.ReadLine();

                Console.Write("new BookStoreName: ");
                string BookStoreName = Console.ReadLine();

                BookStore r = rest.Get<BookStore>(int.Parse(BookStoreId),"BookStore");
                r.BookStoreName = BookStoreName;
                r.ReaderId = int.Parse(ReaderId);
                r.BookId = int.Parse(BookId);
                r.BookStoreId = int.Parse(BookStoreId);
                rest.Put(r,"BookStore");
            }
            if (entity == "Author")
            {
                Console.Write("Id: ");
                string id = Console.ReadLine();

                Console.Write("new name: ");
                string name = Console.ReadLine();

                Author d = rest.Get<Author>(int.Parse(id),"Author");
                d.AuthorName = name;
                rest.Put(d,"Author");
            }
            if (entity == "Book")
            {
                Console.Write("Id: ");
                string BookId = Console.ReadLine();

                Console.Write("new Title: ");
                string Title = Console.ReadLine();

                Console.Write("new Price: ");
                string Income = Console.ReadLine();

                Console.Write("new AuthorId: ");
                string AuthorId = Console.ReadLine();

                Console.Write("new Release: ");
                string Release = Console.ReadLine();

                Console.Write("new Rating: ");
                string Rating = Console.ReadLine();

                Book m = rest.Get<Book>(int.Parse(BookId),"Book");
                m.Title = Title;
                m.Price = int.Parse(Income);
                m.AuthorId = int.Parse(AuthorId);
                m.Release = DateTime.Parse(Release);
                m.Rating = int.Parse(Rating);
                rest.Put(m,"Book");
            }
            Console.WriteLine(entity + " updated.");

            Console.ReadLine();
        }
        static void Delete(string entity)
        {
            Console.WriteLine("\n"+entity + " delete");

            Console.Write("Id: ");
            int id = int.Parse(Console.ReadLine());

            switch (entity)
            {
                case "Reader": rest.Delete(id,"Reader"); break;

                case "Author": rest.Delete(id, "Author"); break;

                case "Book": rest.Delete(id, "Book"); break;

                case "BookStore": rest.Delete(id, "BookStore"); break;
            }

            Console.WriteLine(entity + " deleted.");
            Console.ReadLine();
        }


        static void StoreList()
        {
            Console.Write("Author's name: ");
            string name = Console.ReadLine();

            Console.WriteLine($"\nYou can buy {name}'s books in these stores:\n");

            IQueryable<Book> Books = rest.Get<Book>("Book").AsQueryable();
            var q = Books
                   .Where(x => x.Author.AuthorName.Equals(name))
                   .SelectMany(x => x.BookStores)
                   .ToList();
            foreach (var item in q)
            {
                Console.WriteLine(item.ToString());
            }
            Console.ReadLine();
        }
        static void ReadersAuthorsAndBooks()
        {
            Console.WriteLine("Lists the number of books owned by the reader, grouped by authors.");
            Console.Write("Reader's name: ");
            string name = Console.ReadLine();
            Console.WriteLine("\nAuthors\t\tBooks");

            IQueryable<Book> Book = rest.Get<Book>("Book").AsQueryable();
            var q = Book
                   .SelectMany(b => b.BookStores.Where(r => r.Reader.ReaderName.Equals(name)).Select(r => b.Author))
                   .GroupBy(a => a.AuthorName)
                   .OrderByDescending(g => g.Count())
                   .Select(g => new AuthorsBookCount()
                   {
                       Name = g.Key,
                       BookCount = g.Count()
                   })
                   .ToList();

            foreach (var item in q)
            {
                Console.WriteLine(item.Name+"\t"+item.BookCount);
            }

            Console.ReadLine();
        }
        static void AuthorsByNumberOfBooks()
        {
            Console.Write("Name\tNumber of books\n");
            IQueryable<Book> Books = rest.Get<Book>("Book").AsQueryable();
            var q = Books
                    .GroupBy(b => b.Author.AuthorName)
                    .OrderByDescending(x => x.Count())
                    .Select(x => new AuthorsBookCount()
                    {
                        Name = x.Key,
                        BookCount = x.Count()
                    });
            
            foreach (var item in q)
            {
                Console.WriteLine(item.Name+"\t\t"+item.BookCount);
            }
            Console.ReadLine();
        }
        static void OwnedBooks()
        {
            Console.Write("Reader's name: ");
            string name = Console.ReadLine();

            Console.WriteLine($"{name} owns the following books: ");

            IQueryable<Book> Books = rest.Get<Book>("Book").AsQueryable();
            var q = Books
                .Where(b => b.BookStores.Any(rb => rb.Reader.ReaderName.Equals(name)))
                .ToList();

            foreach (var item in q)
            {
                Console.WriteLine(item.Title);
            }
            Console.ReadLine();
        }
        static void BooksWritten()
        {
            Console.Write("Authors's name: ");
            string name = Console.ReadLine();

            IQueryable<Book> Books = rest.Get<Book>("Book").AsQueryable();
            var q = Books.Where(b => b.Author.AuthorName.Equals(name)).ToList();

            foreach (var item in q)
            {
                Console.WriteLine(item.ToString());
            }
            Console.ReadLine();
        }


        static void Main(string[] args)
        {
            rest = new RestService("http://localhost:23125/", "Book");

            var ReaderSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Reader"))
                .Add("Create", () => Create("Reader"))
                .Add("Delete", () => Delete("Reader"))
                .Add("Update", () => Update("Reader"))
                .Add("Authors and number of books", () => ReadersAuthorsAndBooks())
                .Add("List owned books", () => OwnedBooks())
                .Add("Exit", ConsoleMenu.Close);

            var BookStoreSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("BookStore"))
                .Add("Create", () => Create("BookStore"))
                .Add("Delete", () => Delete("BookStore"))
                .Add("Update", () => Update("BookStore"))
                .Add("Exit", ConsoleMenu.Close);

            var AuthorSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Author"))
                .Add("Create", () => Create("Author"))
                .Add("Delete", () => Delete("Author"))
                .Add("Update", () => Update("Author"))
                .Add("List of books", () => BooksWritten())
                .Add("Authors listed by number of books", () => AuthorsByNumberOfBooks())
                .Add("Stores", () => StoreList())
                .Add("Exit", ConsoleMenu.Close);

            var BookSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Book"))
                .Add("Create", () => Create("Book"))
                .Add("Delete", () => Delete("Book"))
                .Add("Update", () => Update("Book"))
                .Add("Exit", ConsoleMenu.Close);

            var menu = new ConsoleMenu(args, level: 0)
                .Add("Books", () => BookSubMenu.Show())
                .Add("Readers", () => ReaderSubMenu.Show())
                .Add("BookStores", () => BookStoreSubMenu.Show())
                .Add("Authors", () => AuthorSubMenu.Show())
                .Add("Exit", ConsoleMenu.Close);

            menu.Show();
        }
    }
}
