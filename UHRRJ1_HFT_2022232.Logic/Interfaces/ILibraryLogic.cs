using System.Collections.Generic;
using System.Linq;
using UHRRJ1_HFT_2022232.Models;

namespace UHRRJ1_HFT_2022232.Logic.Interfaces
{
    public interface ILibraryLogic
    {
        void Create(Library item);
        void Delete(int id);
        Library Read(int id);
        IQueryable<Library> ReadAll();
        void Update(Library item);
    }
}