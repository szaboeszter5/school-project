using System.Collections.Generic;
using System.Linq;
using UHRRJ1_HFT_2022232.Models;

namespace UHRRJ1_HFT_2022232.Logic.Interfaces
{
    public interface IAuthorLogic
    {
        void Create(Author item);
        void Delete(int id);
        Author Read(int id);
        IQueryable<Author> ReadAll();
        void Update(Author item);
    }
}