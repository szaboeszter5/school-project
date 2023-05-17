using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UHRRJ1_HFT_2022232.Logic;
using UHRRJ1_HFT_2022232.Models;
using UHRRJ1_HFT_2022232.Repository;

namespace UHRRJ1_HFT_2022232.Test
{
    [TestFixture]
    internal class AuthorLogicTester
    {
        AuthorLogic logic;
        Mock<IRepository<Author>> mockAuthorRepo;

        [SetUp]
        public void Init()
        {
            mockAuthorRepo = new Mock<IRepository<Author>>();
            mockAuthorRepo.Setup(d => d.ReadAll()).Returns(new List<Author>()
            {
                //new Author("1#Jon Favreau")

                new Author("1#AuthorA"),
                new Author("2#AuthorB"),
                new Author("3#AuthorC"),
                new Author("4#AuthorD"),
            }.AsQueryable());
            logic = new AuthorLogic(mockAuthorRepo.Object);
        }
    } 
}
