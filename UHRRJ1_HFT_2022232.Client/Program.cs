using ConsoleTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UHRRJ1_HFT_2022232.Models;

namespace UHRRJ1_HFT_2022232.Client
{
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
            if (entity == "Library")
            {
                Console.Write("BookId: ");
                string BookId = Console.ReadLine();

                Console.Write("ReaderId: ");
                string ReaderId = Console.ReadLine();

                Console.Write("LibraryName: ");
                string LibraryName = Console.ReadLine();

                rest.Post(new Library() 
                { 
                    LibraryName = LibraryName,
                    BookId = int.Parse(BookId),
                    ReaderId = int.Parse(ReaderId),
                }, "Library");
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
            if (entity == "Library")
            {
                Console.WriteLine("id" + "\t" + "Name");
                List<Library> Libraries = rest.Get<Library>("Library");
                foreach (var item in Libraries)
                {
                    Console.WriteLine(item.LibraryId + "\t" + item.LibraryName);
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
            if (entity == "Library")
            {
                Console.Write("LibraryId: ");
                string LibraryId = Console.ReadLine();

                Console.Write("new BookId: ");
                string BookId = Console.ReadLine();

                Console.Write("new ReaderId: ");
                string ReaderId = Console.ReadLine();

                Console.Write("new LibraryName: ");
                string LibraryName = Console.ReadLine();

                Library r = rest.Get<Library>(int.Parse(LibraryId),"Library");
                r.LibraryName = LibraryName;
                r.ReaderId = int.Parse(ReaderId);
                r.BookId = int.Parse(BookId);
                r.LibraryId = int.Parse(LibraryId);
                rest.Put(r,"Library");
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

                case "Library": rest.Delete(id, "Library"); break;
            }

            Console.WriteLine(entity + " deleted.");
            Console.ReadLine();
        }

        static void GetAverageRatePerYear()
        {
            Console.Write("Year: ");
            double year = double.Parse(Console.ReadLine());
            IQueryable<Book> Books = rest.Get<Book>("Book").AsQueryable();
            double result = Books.Where(t => t.Release.Year == year).Average(t => t.Rating);
            Console.WriteLine("Rating: "+result);
            Console.ReadLine();
        }
        static void YearStatistics()
        {
            IQueryable<Book> Books = rest.Get<Book>("Book").AsQueryable();
            var q =from x in Books
                   group x by x.Release.Year into g
                   select new
                   {
                       Year = g.Key,
                       AvgRating = g.Average(t => t.Rating),
                       BookNumber = g.Count()
                   };
            Console.WriteLine("Year:\tRating\tNumber of Books");
            foreach (var item in q)
            {
                Console.WriteLine(item.Year + "\t" + Math.Round(item.AvgRating,2) + "\t"+item.BookNumber);
            }
            Console.ReadLine();
        }

        static void Main(string[] args)
        {
            rest = new RestService("http://localhost:23125/", "Book");

            var Stat = new ConsoleMenu(args, level: 1)
                .Add("Average Rating Per Year",() => GetAverageRatePerYear())
                .Add("Year Statistics", () => YearStatistics())
                .Add("Exit", ConsoleMenu.Close);

            var ReaderSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Reader"))
                .Add("Create", () => Create("Reader"))
                .Add("Delete", () => Delete("Reader"))
                .Add("Update", () => Update("Reader"))
                .Add("Exit", ConsoleMenu.Close);

            var LibrarySubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Library"))
                .Add("Create", () => Create("Library"))
                .Add("Delete", () => Delete("Library"))
                .Add("Update", () => Update("Library"))
                .Add("Exit", ConsoleMenu.Close);

            var AuthorSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Author"))
                .Add("Create", () => Create("Author"))
                .Add("Delete", () => Delete("Author"))
                .Add("Update", () => Update("Author"))
                .Add("Exit", ConsoleMenu.Close);

            var BookSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Book"))
                .Add("Create", () => Create("Book"))
                .Add("Delete", () => Delete("Book"))
                .Add("Update", () => Update("Book"))
                .Add("Statistics", () => Stat.Show())
                .Add("Exit", ConsoleMenu.Close);

            var menu = new ConsoleMenu(args, level: 0)
                .Add("Books", () => BookSubMenu.Show())
                .Add("Readers", () => ReaderSubMenu.Show())
                .Add("Librarys", () => LibrarySubMenu.Show())
                .Add("Authors", () => AuthorSubMenu.Show())
                .Add("Exit", ConsoleMenu.Close);

            menu.Show();
        }
    }
}
