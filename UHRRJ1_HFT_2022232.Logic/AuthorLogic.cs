﻿using System.Linq;
using UHRRJ1_HFT_2022232.Logic.Interfaces;
using UHRRJ1_HFT_2022232.Models;
using UHRRJ1_HFT_2022232.Repository;

namespace UHRRJ1_HFT_2022232.Logic
{
    public class AuthorLogic : IAuthorLogic
    {
        IRepository<Author> repo;

        public AuthorLogic(IRepository<Author> repo)
        {
            this.repo = repo;
        }

        public void Create(Author item)
        {
            this.repo.Create(item);
        }

        public void Delete(int id)
        {
            this.repo.Delete(id);
        }

        public Author Read(int id)
        {
            return this.repo.Read(id);
        }

        public IQueryable<Author> ReadAll()
        {
            return this.repo.ReadAll();
        }

        public void Update(Author item)
        {
            this.repo.Update(item);
        }
    }
}
