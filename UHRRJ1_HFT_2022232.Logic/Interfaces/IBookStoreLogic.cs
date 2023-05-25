using System.Collections.Generic;
using System.Linq;
using UHRRJ1_HFT_2022232.Models;

namespace UHRRJ1_HFT_2022232.Logic.Interfaces
{
    public interface IBookStoreLogic
    {
        void Create(BookStore item);
        void Delete(int id);
        BookStore Read(int id);
        IQueryable<BookStore> ReadAll();
        void Update(BookStore item);
        public IEnumerable<Book> OwnedBooks(string readerName);
    }
}