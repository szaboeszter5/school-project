using System.Collections.Generic;
using System.Linq;
using UHRRJ1_HFT_2022232.Models;

namespace UHRRJ1_HFT_2022232.Logic.Interfaces
{
    public interface IMovieLogic
    {
        void Create(Movie item);
        void Delete(int id);
        double? GetAverageRatePerYear(int year);
        Movie Read(int id);
        IQueryable<Movie> ReadAll();
        void Update(Movie item);
        IEnumerable<MovieLogic.YearInfo> YearStatistics();
    }
}