using System.Collections.Generic;
using System.Linq;
using UHRRJ1_HFT_2022232.Models;

namespace UHRRJ1_HFT_2022232.Logic.Interfaces
{
    public interface IBookLogic
    {
        void Create(Book item);
        void Delete(int id);
        double? GetAverageRatePerYear(int year);
        Book Read(int id);
        IQueryable<Book> ReadAll();
        void Update(Book item);
        IEnumerable<BookLogic.YearInfo> YearStatistics();
    }
}