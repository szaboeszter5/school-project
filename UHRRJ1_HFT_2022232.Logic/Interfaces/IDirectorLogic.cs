using System.Collections.Generic;
using System.Linq;
using UHRRJ1_HFT_2022232.Models;

namespace UHRRJ1_HFT_2022232.Logic.Interfaces
{
    public interface IDirectorLogic
    {
        void Create(Director item);
        void Delete(int id);
        Director Read(int id);
        IQueryable<Director> ReadAll();
        void Update(Director item);
    }
}