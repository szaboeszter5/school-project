using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System.Collections.Generic;
using System.Linq;
using UHRRJ1_HFT_2022232.Logic;
using UHRRJ1_HFT_2022232.Logic.Interfaces;
using UHRRJ1_HFT_2022232.Models;
using UHRRJ1_HFT_2022232.Repository;
using static UHRRJ1_HFT_2022232.Logic.BookLogic;

namespace UHRRJ1_HFT_2022232.Test
{
    [TestFixture]
    public class BookLogicTester
    {
        BookLogic booklogic;
        Mock<IRepository<Book>> mockBookRepo;

        [SetUp]
        public void Init()
        {
            mockBookRepo = new Mock<IRepository<Book>>();
            mockBookRepo.Setup(d => d.ReadAll()).Returns(new List<Book>()
            {
                new Book ("1,Book1,1000,5,2000.01.01,1")
                {
                    Author = new Author ("1,Author 1"),
                    BookStores = new List<BookStore>()
                    {
                        new BookStore ("1,Book Store 1,1,1")
                        {
                            Reader = new Reader ("1,Reader 1")
                        },
                    },
                    Readers = new List<Reader>()
                    {
                        new Reader ("1,Reader 1")
                    }
                },


                new Book ("2,Book2,2000,4,2010.02.03,1")
                {
                    Author = new Author("1,Author 1"),
                    BookStores = new List<BookStore>()
                    {
                        new BookStore ("2,Book Store 1,2,1")
                        {
                            Reader = new Reader ("1,Reader 1")
                        },
                    },
                    Readers = new List<Reader>()
                    {
                        new Reader ("1,Reader 1")
                    }
                },


                new Book ("3,Book3,3000,3,2013.01.04,1")
                {
                    Author = new Author("1,Author 1"),
                    BookStores = new List<BookStore>()
                    {
                        new BookStore ("3,Book Store 1,3,2")
                        { 
                            Reader = new Reader ("2,Reader 2"),
                        },
                        new BookStore ("6,Book Store 2,3,3")
                        {
                            Reader =new Reader ("3,Reader 3")
                        },
                    },
                    Readers = new List<Reader>()
                    {
                        new Reader ("2,Reader 2"),
                        new Reader ("3,Reader 3")
                    }
                },


                new Book ("4,Book4,1100,2,1997.10.10,2")
                {
                    Author = new Author("2,Author 2"),
                    BookStores = new List<BookStore>()
                    {
                        new BookStore ("4,Book Store 1,4,2")
                        {
                            Reader = new Reader ("2,Reader 2")
                        },
                        new BookStore ("7,Book Store 2,4,4")
                        {
                            Reader = new Reader ("4,Reader 4")
                        },
                    },
                    Readers = new List<Reader>()
                    {
                        new Reader ("2,Reader 2"),
                        new Reader ("4,Reader 4")
                    }
                },


                new Book ("5,Book5,1200,1,1996.08.23,2")
                {
                    Author = new Author("2,Author 2"),
                    BookStores = new List<BookStore>()
                    {
                        new BookStore ("8,Book Store 2,5,4")
                        { 
                            Reader = new Reader ("4,Reader 4")
                        },
                    },
                    Readers = new List<Reader>()
                    {
                        new Reader ("4,Reader 4")
                    }
                },


                new Book ("6,Book6,1300,5,1899.03.06,3")
                { 
                    Author = new Author("3,Author 3"),
                    BookStores = new List<BookStore>()
                    {
                        new BookStore ("9,Book Store 3,6,1")
                        {
                            Reader = new Reader ("1,Reader 1")
                        },
                    },
                    Readers = new List<Reader>()
                    {
                        new Reader ("1,Reader 1")
                    }
                },


                new Book ("7,Book7,1400,4,1888.10.30,3")
                { 
                    Author = new Author("3,Author 3"),
                    BookStores = new List<BookStore>()
                    {
                        new BookStore ("10,Book Store 3,7,1")
                        {
                            Reader = new Reader ("1,Reader 1")
                        },
                    },
                    Readers = new List<Reader>()
                    {
                        new Reader ("1,Reader 1")
                    }
                },


                new Book ("8,Book8,1500,3,1866.11.03,4")
                { 
                    Author = new Author("4,Author 4"),
                    BookStores = new List<BookStore>()
                    {
                        new BookStore ("11,Book Store 3,8,3")
                        {
                         Reader = new Reader ("3,Reader 3")
                        },
                    },
                    Readers = new List<Reader>()
                    {
                        new Reader ("3,Reader 3")
                    }
                },


                new Book ("9,Book9,1600,2,2000.03.05,4") 
                { 
                    Author = new Author("4,Author 4"),
                    BookStores = new List<BookStore>()
                    {
                        new BookStore ("12,Book Store 3,9,4")
                        {Reader = new Reader("4,Reader 4")},
                    },
                    Readers = new List<Reader>()
                    {
                        new Reader ("4,Reader 4")
                    }
                },


                new Book ("10,Book10,1700,1,1999.06.07,4")
                {
                    Author = new Author("4,Author 4"),
                    BookStores = new List<BookStore>()
                    {
                        new BookStore ("13,Book Store 3,10,1")
                        {Reader = new Reader("1,Reader 1")}
                    },
                    Readers = new List<Reader>()
                    {
                        new Reader ("1,Reader 1")
                    }
                }

            }.AsQueryable()) ;
            booklogic = new BookLogic(mockBookRepo.Object);
        }



        //CREATE EXCEPTION HANDLING
        [Test]
        public void CreateBookTestWithCorrectTitle()
        {
            var Book = new Book() { Title = "A per" };

            //ACT
            booklogic.Create(Book);

            //ASSERT
            mockBookRepo.Verify(r => r.Create(Book), Times.Once);
        }

        [Test]
        public void CreateBookTestWithIncorrectTitle()
        {
            var Book = new Book() { Title = "Az" };
            try
            {
                //ACT
                booklogic.Create(Book);
            }
            catch
            {

            }

            //ASSERT
            mockBookRepo.Verify(r => r.Create(Book), Times.Never);
        }



        //NON-CRUD
        [Test]
        public void StoresTest()
        {
            IEnumerable<BookStore> actual = booklogic.Stores("Author 1");
            IEnumerable<BookStore> expected = new List<BookStore>
            {
                new BookStore ("1,Book Store 1,1,1"),
                new BookStore ("2,Book Store 1,2,1"),
                new BookStore ("3,Book Store 1,3,2"),
                new BookStore ("6,Book Store 2,3,3")
            };

            Assert.That(actual, Is.EquivalentTo(expected));
        }

        [Test]
        public void ReadersAuthorsAndBooks()
        {
            IEnumerable<AuthorsBookCount> actual = booklogic.ReadersAuthorsAndBooks("Reader 1");
            IEnumerable<AuthorsBookCount> expected = new List<AuthorsBookCount>
            {
                new AuthorsBookCount("Author 1",2),
                new AuthorsBookCount("Author 3",2),
                new AuthorsBookCount("Author 4",1)
            };

            Assert.That(actual, Is.EquivalentTo(expected));
        }

        [Test]
        public void AuthorsByNumberOfBooksTest()
        {
            IEnumerable<AuthorsBookCount> actual = booklogic.AuthorsByNumberOfBooks();
            IEnumerable<AuthorsBookCount> expected = new List<AuthorsBookCount>
            {
                new AuthorsBookCount ("Author 1",3),
                new AuthorsBookCount ("Author 2",2),
                new AuthorsBookCount ("Author 3",2),
                new AuthorsBookCount ("Author 4",3)
            };

            Assert.That(actual, Is.EquivalentTo(expected));
        }

        [Test]
        public void OwnedBooksTest()
        {
            IEnumerable<Book> actual = booklogic.OwnedBooks("Reader 1");
            IEnumerable<Book> expected = new List<Book>
            {
                new Book ("1,Book1,1000,5,2000.01.01,1"),
                new Book ("2,Book2,2000,4,2010.02.03,1"),
                new Book ("6,Book6,1300,5,1899.03.06,3"),
                new Book ("7,Book7,1400,4,1888.10.30,3"),
                new Book ("10,Book10,1700,1,1999.06.07,4")
            };

            Assert.That(actual, Is.EquivalentTo(expected));
        }

        [Test]
        public void BooksWrittenTest()
        {
            IEnumerable<Book> actual = booklogic.BooksWritten("Author 1");
            IEnumerable<Book> expected = new List<Book>
            {
                new Book ("1,Book1,1000,5,2000.01.01,1"),
                new Book ("2,Book2,2000,4,2010.02.03,1"),
                new Book ("3,Book3,3000,3,2013.01.04,1")
            };

            Assert.That(actual, Is.EquivalentTo(expected));
        }
    }
}
