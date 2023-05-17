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
    public class LibraryLogicTester
    {
        LibraryLogic logic;
        Mock<IRepository<Library>> mockLibraryRepo;

        [SetUp]
        public void Init()
        {
            mockLibraryRepo = new Mock<IRepository<Library>>();
            mockLibraryRepo.Setup(m => m.ReadAll()).Returns(new List<Library>()
            {
                new Library("1#1#1#1#Library1"),
                new Library("2#2#2#2#Library2"),
                new Library("3#3#3#3#Library3"),
                new Library("4#4#4#4#Library4"),
            }.AsQueryable());
            logic = new LibraryLogic(mockLibraryRepo.Object);
        }


    }
}
