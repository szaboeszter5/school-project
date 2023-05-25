using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UHRRJ1_HFT_2022232.Logic;
using UHRRJ1_HFT_2022232.Models;
using UHRRJ1_HFT_2022232.Repository;

namespace UHRRJ1_HFT_2022232.Test
{
    [TestFixture]
    public class BookStoreLogicTester
    {
        BookStoreLogic bookstorelogic;
        Mock<IRepository<BookStore>> mockBookStoreRepo;

        [SetUp]
        public void Init()
        {
            mockBookStoreRepo = new Mock<IRepository<BookStore>>();
            mockBookStoreRepo.Setup(d => d.ReadAll()).Returns(new List<BookStore>()
            {
                new BookStore("1,Book Store 1,1,1")
                {
                    Reader = new Reader("1,Reader 1"),
                    Book = new Book("1,Book1,1000,5,2000.01.01,1")
                },

                new BookStore ("2,Book Store 1,2,1")
                {
                    Reader = new Reader("1,Reader 1"),
                    Book = new Book ("2,Book2,2000,4,2010.02.03,1")
                },

                new BookStore ("3,Book Store 1,3,2")
                {
                    Reader = new Reader ("2,Reader 2"),
                    Book = new Book ("3,Book3,3000,3,2013.01.04,1")
                },

                new BookStore ("6,Book Store 2,3,3")
                {
                    Reader = new Reader ("3,Reader 3"),
                    Book = new Book ("3,Book3,3000,3,2013.01.04,1")
                }

            }.AsQueryable());
            bookstorelogic = new BookStoreLogic(mockBookStoreRepo.Object);

        }

        
    }
}
