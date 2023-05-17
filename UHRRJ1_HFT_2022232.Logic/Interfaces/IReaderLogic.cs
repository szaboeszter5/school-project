using System.Collections.Generic;
using System.Linq;
using UHRRJ1_HFT_2022232.Models;

namespace UHRRJ1_HFT_2022232.Logic
{
    public interface IReaderLogic
    {
        void Create(Reader item);
        void Delete(int id);
        Reader Read(int id);
        IQueryable<Reader> ReadAll();
        void Update(Reader item);
    }
}