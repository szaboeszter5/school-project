using System.Collections.Generic;
using System.Linq;
using UHRRJ1_HFT_2022232.Logic.Interfaces;
using UHRRJ1_HFT_2022232.Models;
using UHRRJ1_HFT_2022232.Repository;

namespace UHRRJ1_HFT_2022232.Logic
{
    public class BookStoreLogic : IBookStoreLogic
    {
        IRepository<BookStore> repo;

        public BookStoreLogic(IRepository<BookStore> repo)
        {
            this.repo = repo;
        }

        #region CRUD
        public void Create(BookStore item)
        {
            this.repo.Create(item);
        }

        public void Delete(int id)
        {
            this.repo.Delete(id);
        }

        public BookStore Read(int id)
        {
            return this.repo.Read(id);
        }

        public IQueryable<BookStore> ReadAll()
        {
            return this.repo.ReadAll();
        }

        public void Update(BookStore item)
        {
            this.repo.Update(item);
        }
        #endregion


        //milyen könyveket vett meg
        public IEnumerable<Book> OwnedBooks(string readerName)
        {
            return repo.ReadAll()
                .Where(x=>x.Reader.ReaderName.Equals(readerName))
                .Select(x=>x.Book);
        }
    }
}
