using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UHRRJ1_HFT_2022232.Logic;
using UHRRJ1_HFT_2022232.Models;
using UHRRJ1_HFT_2022232.Repository;
using static UHRRJ1_HFT_2022232.Logic.BookLogic;

namespace UHRRJ1_HFT_2022232.Test
{
    [TestFixture]
    public class BookLogicTester
    {
        BookLogic logic;
        Mock<IRepository<Book>> mockBookRepo;

        [SetUp]
        public void Init()
        {
            mockBookRepo = new Mock<IRepository<Book>>();
            mockBookRepo.Setup(m => m.ReadAll()).Returns(new List<Book>()
            {
                new Book("1#BookA#100#1#2008*05*02#5"),
                new Book("2#BookB#200#1#2009*05*02#6"),
                new Book("3#BookC#300#1#2009*05*02#7"),
                new Book("4#BookD#400#1#2010*05*02#8"),
            }.AsQueryable());
            logic = new BookLogic(mockBookRepo.Object);
        }

        [Test]
        public void AvgRatePerYearTest()
        {
            double? avg = logic.GetAverageRatePerYear(2009);
            Assert.That(avg, Is.EqualTo(6.5));
        }

        [Test]
        public void YearStatisticsTest()
        {
            var actual = logic.YearStatistics().ToList();
            var expected = new List<YearInfo>()
            {
                new YearInfo()
                {
                    Year = 2008,
                    AvgRating = 5,
                    BookNumber = 1
                },
                new YearInfo()
                {
                    Year = 2009,
                    AvgRating = 6.5,
                    BookNumber = 2
                },
                new YearInfo()
                {
                    Year = 2010,
                    AvgRating = 8,
                    BookNumber = 1
                }
            };

            Assert.AreEqual(expected, actual);
        }


        //CREATE EXCEPTION HANDLING
        [Test]
        public void CreateBookTestWithCorrectTitle()
        {
            var Book = new Book() { Title = "A per" };

            //ACT
            logic.Create(Book);

            //ASSERT
            mockBookRepo.Verify(r => r.Create(Book), Times.Once);
        }

        [Test]
        public void CreateBookTestWithInCorrectTitle()
        {
            var Book = new Book() { Title = "Az" };
            try
            {
                //ACT
                logic.Create(Book);
            }
            catch
            {

            }

            //ASSERT
            mockBookRepo.Verify(r => r.Create(Book), Times.Never);
        }
    }
}
