using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UHRRJ1_HFT_2022232.Logic;
using UHRRJ1_HFT_2022232.Logic.Interfaces;
using UHRRJ1_HFT_2022232.Models;
using UHRRJ1_HFT_2022232.Repository;
using static UHRRJ1_HFT_2022232.Logic.ReaderLogic;

namespace UHRRJ1_HFT_2022232.Test
{
    [TestFixture]
    public class ReaderLogicTester
    {
        ReaderLogic readerlogic;
        Mock<IRepository<Reader>> mockReaderRepo;

        [SetUp]
        public void Init()
        {
            mockReaderRepo = new Mock<IRepository<Reader>>();
            mockReaderRepo.Setup(d => d.ReadAll()).Returns(new List<Reader>()
            {
                new Reader ("1,Béla")
                {
                    Books = new List<Book>()
                    {
                        new Book ("1,Book1,1000,5,2000.01.01,1"),
                        new Book ("2,Book2,2000,4,2010.02.03,1")
                    }
                },
                new Reader ("2,Géza"),
                new Reader ("3,Judit"),
                new Reader ("4,Kata")
            }.AsQueryable());
            readerlogic = new ReaderLogic(mockReaderRepo.Object);
        }

        //CREATE EXCEPTION HANDLING
        [Test]
        public void CreateReader_ValidName_Test()
        {
            var Reader = new Reader()
            {
                ReaderName = "Béla"
            };

            //ACT
            readerlogic.Create(Reader);

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
                readerlogic.Create(Reader);
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
                readerlogic.Create(Reader);
            }
            catch { }
            //ASSERT
            mockReaderRepo.Verify(r => r.Create(Reader), Times.Never);
        }
    }
}