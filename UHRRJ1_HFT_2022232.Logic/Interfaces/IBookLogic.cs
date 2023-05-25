using System.Collections.Generic;
using System.Linq;
using UHRRJ1_HFT_2022232.Models;
using static UHRRJ1_HFT_2022232.Logic.BookLogic;

namespace UHRRJ1_HFT_2022232.Logic.Interfaces
{
    public interface IBookLogic
    {
        void Create(Book item);
        void Delete(int id);
        Book Read(int id);
        IQueryable<Book> ReadAll();
        void Update(Book item);
        IEnumerable<BookStore> Stores(string authorName);
        public IEnumerable<AuthorsBookCount> ReadersAuthorsAndBooks(string readerName);
        public IEnumerable<AuthorsBookCount> AuthorsByNumberOfBooks();
        public IEnumerable<Book> OwnedBooks(string readerName);
        public IEnumerable<Book> BooksWritten(string authorName);

    }
}