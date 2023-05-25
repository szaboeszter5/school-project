using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using UHRRJ1_HFT_2022232.Logic.Interfaces;
using UHRRJ1_HFT_2022232.Models;
using static UHRRJ1_HFT_2022232.Logic.BookLogic;

namespace UHRRJ1_HFT_2022232.Endpoint.Controllers.non_crud
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class AuthorBookNumberController : ControllerBase
    {
        IBookLogic bookLogic;

        public AuthorBookNumberController(IBookLogic bookLogic)
        {
            this.bookLogic = bookLogic;
        }

        [HttpGet]
        public IEnumerable<AuthorsBookCount> AuthorsByNumberOfBooks()
        {
            return bookLogic.AuthorsByNumberOfBooks();
        }
    }
}
