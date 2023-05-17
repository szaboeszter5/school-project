using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UHRRJ1_HFT_2022232.Models;
using UHRRJ1_HFT_2022232.Repository;

namespace UHRRJ1_HFT_2022232.Logic
{
    public class ReaderLogic : IReaderLogic
    {
        IRepository<Reader> repo;

        public ReaderLogic(IRepository<Reader> repo)
        {
            this.repo = repo;
        }

        #region CRUD
        public void Create(Reader item)
        {
            if (item.ReaderName == null || item.ReaderName == "") throw new ArgumentNullException();
            else repo.Create(item);
        }

        public void Delete(int id)
        {
            this.repo.Delete(id);
        }

        public Reader Read(int id)
        {
            return this.repo.Read(id);
        }

        public IQueryable<Reader> ReadAll()
        {
            return this.repo.ReadAll();
        }

        public void Update(Reader item)
        {
            this.repo.Update(item);
        }
        #endregion
    }
}
