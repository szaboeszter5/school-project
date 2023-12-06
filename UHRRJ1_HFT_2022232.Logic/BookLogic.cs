using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Xml.Linq;
using UHRRJ1_HFT_2022232.Logic.Interfaces;
using UHRRJ1_HFT_2022232.Models;
using UHRRJ1_HFT_2022232.Repository;

namespace UHRRJ1_HFT_2022232.Logic
{
    public class BookLogic : IBookLogic
    {
        IRepository<Book> repo;

        public BookLogic(IRepository<Book> repo)
        {
            this.repo = repo;
        }

        #region CRUD
        public void Create(Book item)
        {
            this.repo.Create(item);
        }

        public void Delete(int id)
        {
            this.repo.Delete(id);
        }

        public Book Read(int id)
        {
            return this.repo.Read(id);
        }

        public IQueryable<Book> ReadAll()
        {
            return this.repo.ReadAll();
        }

        public void Update(Book item)
        {
            this.repo.Update(item);
        }
        #endregion

        //mely boltokban lehet megvenni author könyveit
        public IEnumerable<BookStore> Stores(string authorname)
        {
            return repo.ReadAll()
                   .Where(x => x.Author.AuthorName.Equals(authorname))
                   .SelectMany(x => x.BookStores)
                   .ToList();
        }

        //a reader írói és a könyvek száma
        public IEnumerable<AuthorsBookCount> ReadersAuthorsAndBooks(string readerName)
        {
            return
                   repo.ReadAll()
                   .SelectMany(b => b.BookStores.Where(r => r.Reader.ReaderName.Equals(readerName)).Select(r => b.Author))
                   .GroupBy(a => a.AuthorName)
                   .OrderByDescending(g => g.Count())
                   .Select(g => new AuthorsBookCount(null,0)
                   {
                       Name = g.Key,
                       BookCount = g.Count()
                   })
                   .ToList();
        }

        //authorok listázva könyvek száma szerint
        public IEnumerable<AuthorsBookCount> AuthorsByNumberOfBooks()
        {
            return repo.ReadAll()
                   .GroupBy(b => b.Author.AuthorName)
                   .OrderByDescending(x => x.Count())
                   .Select(x => new AuthorsBookCount(null,0)
                   {
                       Name = x.Key,
                       BookCount = x.Count()
                   });
        }

        //egy reader összes könyve
        public IEnumerable<Book> OwnedBooks(string readerName)
        {
            return repo.ReadAll().Where(b => b.BookStores.Any(rb => rb.Reader.ReaderName.Equals(readerName))).ToList();
         
        }

        //list all books of the author
        public IEnumerable<Book> BooksWritten(string authorName)
        { 
            return repo.ReadAll().Where(b => b.Author.AuthorName.Equals(authorName)).ToList();
        }

        public class AuthorsBookCount
        {
            public string Name { get; internal set; }
            public int BookCount { get; internal set; }

            public AuthorsBookCount(string name, int bookCount)
            {
                BookCount = bookCount;
                Name = name;
            }

            public override bool Equals(object obj)
            {
                AuthorsBookCount other = obj as AuthorsBookCount;
                if (other != null && other.Name != null)
                {
                    return Name.Equals(other.Name)
                        && BookCount == other.BookCount;
                }
                else
                {
                    return false;
                }
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(Name, BookCount);
            }
        }
    }
}
    
