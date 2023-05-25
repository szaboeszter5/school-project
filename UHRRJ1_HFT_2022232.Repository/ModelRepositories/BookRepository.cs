using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UHRRJ1_HFT_2022232.Models;

namespace UHRRJ1_HFT_2022232.Repository
{
    public class BookRepository : Repository<Book>, IRepository<Book>
    {
        public BookRepository(BookStoreDbContext ctx) : base(ctx)
        {
        }

        public override Book Read(int id)
        {
            return ctx.Books.FirstOrDefault(t => t.BookId == id);
        }

        public override void Update(Book item)
        {
            var old = Read(item.BookId);
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
