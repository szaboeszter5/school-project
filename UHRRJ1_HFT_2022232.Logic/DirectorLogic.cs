using System.Linq;
using UHRRJ1_HFT_2022232.Logic.Interfaces;
using UHRRJ1_HFT_2022232.Models;
using UHRRJ1_HFT_2022232.Repository;

namespace UHRRJ1_HFT_2022232.Logic
{
    public class DirectorLogic : IDirectorLogic
    {
        IRepository<Director> repo;

        public DirectorLogic(IRepository<Director> repo)
        {
            this.repo = repo;
        }

        public void Create(Director item)
        {
            this.repo.Create(item);
        }

        public void Delete(int id)
        {
            this.repo.Delete(id);
        }

        public Director Read(int id)
        {
            return this.repo.Read(id);
        }

        public IQueryable<Director> ReadAll()
        {
            return this.repo.ReadAll();
        }

        public void Update(Director item)
        {
            this.repo.Update(item);
        }
    }
}
