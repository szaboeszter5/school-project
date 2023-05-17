using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UHRRJ1_HFT_2022232.Models;

namespace UHRRJ1_HFT_2022232.Repository
{
    public class LibraryRepository : Repository<Library>, IRepository<Library>
    {
        public LibraryRepository(LibraryDbContext ctx) : base(ctx)
        {
        }

        public override Library Read(int id)
        {
            return ctx.Libraries.FirstOrDefault(t => t.LibraryId == id);
        }

        public override void Update(Library item)
        {
            var old = Read(item.LibraryId);
            foreach (var prop in old.GetType().GetProperties())
            {
                if (prop.GetAccessors().FirstOrDefault(t => t.IsVirtual) == null)
                {
                    prop.SetValue(old, prop.GetValue(item));
                }
            }
            ctx.SaveChanges();
        }
    }
}
