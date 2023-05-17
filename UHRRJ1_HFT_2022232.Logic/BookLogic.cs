using System;
using System.Collections.Generic;
using System.Linq;
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
            if (item.Title.Length < 3)
            {
                throw new ArgumentException();
            }
            this.repo.Create(item);
        }

        public void Delete(int id)
        {
            this.repo.Delete(id);
        }

        public Book Read(int id)
        {
            var Book = this.repo.Read(id);
            if (Book == null)
            {
                throw new ArgumentException("This Book does not exist.");
            }
            return Book;
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

        public double? GetAverageRatePerYear(int year)
        {
            return this.repo
               .ReadAll()
               .Where(t => t.Release.Year == year)
               .Average(t => t.Rating);
        }

        public IEnumerable<YearInfo> YearStatistics()
        {
            return from x in this.repo.ReadAll()
                   group x by x.Release.Year into g
                   select new YearInfo()
                   {
                       Year = g.Key,
                       AvgRating = g.Average(t => t.Rating),
                       BookNumber = g.Count()
                   };
        }

        public class YearInfo
        {
            public int Year { get; set; }
            public double? AvgRating { get; set; }
            public int BookNumber { get; set; }

            public override bool Equals(object obj)
            {
                YearInfo other = obj as YearInfo;
                if (other == null) return false;
                return Year == other.Year && AvgRating == other.AvgRating && BookNumber == other.BookNumber;
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(Year,AvgRating,BookNumber);
            }
        }
    }
}
