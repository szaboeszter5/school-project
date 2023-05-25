using System.Linq;
using UHRRJ1_HFT_2022232.Models;

namespace UHRRJ1_HFT_2022232.Repository
{
    public class ReaderRepository : Repository<Reader>, IRepository<Reader>
    {
        public ReaderRepository(BookStoreDbContext ctx) : base(ctx)
        {
        }

        public override Reader Read(int id)
        {
            return ctx.Readers.FirstOrDefault(t => t.ReaderId == id);
        }

        public override void Update(Reader item)
        {
            var old = Read(item.ReaderId);
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
