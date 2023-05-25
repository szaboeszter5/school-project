using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UHRRJ1_HFT_2022232.Models;

namespace UHRRJ1_HFT_2022232.Repository
{
    public class LibraryRepository : Repository<BookStore>, IRepository<BookStore>
    {
        public LibraryRepository(BookStoreDbContext ctx) : base(ctx)
        {
        }

        public override BookStore Read(int id)
        {
            return ctx.BookStores.FirstOrDefault(t => t.BookStoreId == id);
        }

        public override void Update(BookStore item)
        {
            var old = Read(item.BookStoreId);
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
