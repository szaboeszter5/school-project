using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UHRRJ1_HFT_2022232.Logic;
using UHRRJ1_HFT_2022232.Models;
using UHRRJ1_HFT_2022232.Repository;
using static UHRRJ1_HFT_2022232.Logic.ReaderLogic;

namespace UHRRJ1_HFT_2022232.Test
{
    [TestFixture]
    public class ReaderLogicTester
    {
        ReaderLogic logic;
        Mock<IRepository<Reader>> mockReaderRepo;

        [SetUp]
        public void Init()
        {
            mockReaderRepo = new Mock<IRepository<Reader>>();
            mockReaderRepo.Setup(a => a.ReadAll()).Returns(new List<Reader>()
            {
                //new Library("1#1#212#1#Tony Stark")

                new Reader()
                {
                    ReaderId = 1,
                    ReaderName = "ReaderA",
                    Libraries = new List<Library>()
                    {
                        new Library()
                    }
                },
                new Reader()
                {
                    ReaderId=2,
                    ReaderName="ReaderB",
                    Libraries = new List<Library>()
                    {
                        new Library(""),
                        new Library("")
                    }
                },
                new Reader()
                {
                    ReaderId = 3,
                    ReaderName = "ReaderC",
                    Libraries = new List<Library>()
                    {
                        new Library()
                    }
                },
                new Reader()
                {
                    ReaderId = 4,
                    ReaderName = "ReaderD",
                    Libraries = new List<Library>()
                    {
                    }
                }
            }.AsQueryable());
            logic = new ReaderLogic(mockReaderRepo.Object);
        }

        //CREATE EXCEPTION HANDLING
        [Test]
        public void CreateReader_ValidName_Test()
        {
            var Reader = new Reader() { ReaderName = "Géza" };

            //ACT
            logic.Create(Reader);

            //ASSERT
            mockReaderRepo.Verify(r => r.Create(Reader), Times.Once);
        }

        [Test]
        public void CreateReader_NameIsEmptyString()
        {
            var Reader = new Reader() { ReaderName = "" };

            //ACT
            try
            {
                logic.Create(Reader);
            }
            catch { }
            //ASSERT
            mockReaderRepo.Verify(r => r.Create(Reader), Times.Never);
        }

        [Test]
        public void CreateReader_NameIsNull()
        {
            var Reader = new Reader();

            //ACT
            try
            {
                logic.Create(Reader);
            }
            catch { }
            //ASSERT
            mockReaderRepo.Verify(r => r.Create(Reader), Times.Never);
        }
    }
}