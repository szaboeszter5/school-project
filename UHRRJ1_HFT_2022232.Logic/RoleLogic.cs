using System.Linq;
using UHRRJ1_HFT_2022232.Logic.Interfaces;
using UHRRJ1_HFT_2022232.Models;
using UHRRJ1_HFT_2022232.Repository;

namespace UHRRJ1_HFT_2022232.Logic
{
    public class RoleLogic : IRoleLogic
    {
        IRepository<Role> repo;

        public RoleLogic(IRepository<Role> repo)
        {
            this.repo = repo;
        }

        public void Create(Role item)
        {
            this.repo.Create(item);
        }

        public void Delete(int id)
        {
            this.repo.Delete(id);
        }

        public Role Read(int id)
        {
            return this.repo.Read(id);
        }

        public IQueryable<Role> ReadAll()
        {
            return this.repo.ReadAll();
        }

        public void Update(Role item)
        {
            this.repo.Update(item);
        }
    }
}
