﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UHRRJ1_HFT_2022232.Models;

namespace UHRRJ1_HFT_2022232.Repository
{
    public class AuthorRepository : Repository<Author>, IRepository<Author>
    {
        public AuthorRepository(BookStoreDbContext ctx) : base(ctx)
        {
        }

        public override Author Read(int id)
        {
            return ctx.Authors.FirstOrDefault(t => t.AuthorId == id);
        }

        public override void Update(Author item)
        {
            var old = Read(item.AuthorId);
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
