using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using UHRRJ1_HFT_2022232.Logic.Interfaces;
using UHRRJ1_HFT_2022232.Models;

namespace UHRRJ1_HFT_2022232.Endpoint.Controllers.non_crud
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class WrittenBooksController : ControllerBase
    {
        IBookLogic bookLogic;

        public WrittenBooksController(IBookLogic bookLogic)
        {
            this.bookLogic = bookLogic;
        }

        [HttpGet]
        public IEnumerable<Book> WrittenBooks(string authorName)
        {
            return bookLogic.BooksWritten(authorName);
        }

    }
}
