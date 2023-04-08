using System.Linq;
using UHRRJ1_HFT_2022232.Logic;
using UHRRJ1_HFT_2022232.Models;
using UHRRJ1_HFT_2022232.Repository;
using UHRRJ1_HFT_2022232.Repository.MovieDbApp.Repository;

namespace UHRRJ1_HFT_2022232.Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var ctx = new MovieDbContext();
            var repo = new MovieRepository(ctx);
            var logic = new MovieLogic(repo);

            ;
        }
    }
}
