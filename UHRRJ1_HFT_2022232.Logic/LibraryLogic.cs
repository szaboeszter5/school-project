using System.Linq;
using UHRRJ1_HFT_2022232.Logic.Interfaces;
using UHRRJ1_HFT_2022232.Models;
using UHRRJ1_HFT_2022232.Repository;

namespace UHRRJ1_HFT_2022232.Logic
{
    public class LibraryLogic : ILibraryLogic
    {
        IRepository<Library> repo;

        public LibraryLogic(IRepository<Library> repo)
        {
            this.repo = repo;
        }

        public void Create(Library item)
        {
            this.repo.Create(item);
        }

        public void Delete(int id)
        {
            this.repo.Delete(id);
        }

        public Library Read(int id)
        {
            return this.repo.Read(id);
        }

        public IQueryable<Library> ReadAll()
        {
            return this.repo.ReadAll();
        }

        public void Update(Library item)
        {
            this.repo.Update(item);
        }
    }
}
