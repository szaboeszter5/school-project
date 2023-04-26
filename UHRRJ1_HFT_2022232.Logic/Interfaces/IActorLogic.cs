using System.Collections.Generic;
using System.Linq;
using UHRRJ1_HFT_2022232.Models;

namespace UHRRJ1_HFT_2022232.Logic
{
    public interface IActorLogic
    {
        void Create(Actor item);
        void Delete(int id);
        Actor Read(int id);
        IQueryable<Actor> ReadAll();
        void Update(Actor item);
    }
}