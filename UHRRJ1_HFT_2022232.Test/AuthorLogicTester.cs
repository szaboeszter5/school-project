using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UHRRJ1_HFT_2022232.Logic;
using UHRRJ1_HFT_2022232.Logic.Interfaces;
using UHRRJ1_HFT_2022232.Models;
using UHRRJ1_HFT_2022232.Repository;

namespace UHRRJ1_HFT_2022232.Test
{
    [TestFixture]
    internal class AuthorLogicTester
    {
        AuthorLogic authorlogic;
        BookLogic booklogic;
        BookStoreLogic bookstorelogic;
        Mock<IRepository<Author>> mockAuthorRepo;
        Mock<IRepository<Book>> mockBookRepo;
        Mock<IRepository<BookStore>> mockBookStoreRepo;


        [SetUp]
        public void Init()
        {
            mockAuthorRepo = new Mock<IRepository<Author>>();
            mockAuthorRepo.Setup(d => d.ReadAll()).Returns(new List<Author>()
            {
                new Author ("1,Author 1"),
                new Author ("2,Author 2"),
                new Author ("3,Author 3"),
                new Author ("4,Author 4"),
                new Author ("5,Author 5")
            }.AsQueryable());
            authorlogic = new AuthorLogic(mockAuthorRepo.Object);

            mockBookRepo = new Mock<IRepository<Book>>();
            mockBookRepo.Setup(d => d.ReadAll()).Returns(new List<Book>()
            {
                new Book ("1,Book1,1000,5,2000.01.01,1"),
                new Book ("2,Book2,2000,4,2010.02.03,1"),
                new Book ("3,Book3,3000,3,2013.01.04,1"),
                new Book ("4,Book4,1100,2,1997.10.10,2"),
                new Book ("5,Book5,1200,1,1996.08.23,2"),
                new Book ("6,Book6,1300,5,1899.03.06,3"),
                new Book ("7,Book7,1400,4,1888.10.30,3"),
                new Book ("8,Book8,1500,3,1866.11.03,4"),
                new Book ("9,Book9,1600,2,2000.03.05,4"),
                new Book ("10,Book10,1700,1,1999.06.07,4")
            }.AsQueryable());
            booklogic = new BookLogic(mockBookRepo.Object);

            mockBookStoreRepo = new Mock<IRepository<BookStore>>();
            mockBookStoreRepo.Setup(d => d.ReadAll()).Returns(new List<BookStore>()
            {
                new BookStore ("1,Book Store 1,1,1"),
                new BookStore ("2,Book Store 1,2,1"),
                new BookStore ("3,Book Store 1,3,2"),
                new BookStore ("4,Book Store 1,4,2"),

                new BookStore ("5,Book Store 2,2,2"),
                new BookStore ("6,Book Store 2,3,3"),
                new BookStore ("7,Book Store 2,4,4"),
                new BookStore ("8,Book Store 2,5,4"),

                new BookStore ("9,Book Store 3,6,1"),
                new BookStore ("10,Book Store 3,7,1"),
                new BookStore ("11,Book Store 3,8,3"),
                new BookStore ("12,Book Store 3,9,4"),
                new BookStore ("13,Book Store 3,10,1")
            }.AsQueryable());
            bookstorelogic = new BookStoreLogic(mockBookStoreRepo.Object);
        }
    }
}
