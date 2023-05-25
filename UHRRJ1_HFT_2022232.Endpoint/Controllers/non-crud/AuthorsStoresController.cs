using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using UHRRJ1_HFT_2022232.Logic.Interfaces;
using UHRRJ1_HFT_2022232.Models;

namespace UHRRJ1_HFT_2022232.Endpoint.Controllers.non_crud
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class AuthorsStoresController : ControllerBase
    {
        IBookLogic bookLogic;

        public AuthorsStoresController(IBookLogic bookLogic)
        {
            this.bookLogic = bookLogic;
        }

        [HttpGet]
        public IEnumerable<BookStore> Stores(string authorname)
        {
            return bookLogic.Stores(authorname);
        }
    }
}
