using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using UHRRJ1_HFT_2022232.Logic.Interfaces;
using UHRRJ1_HFT_2022232.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UHRRJ1_HFT_2022232.Endpoint.Controllers.models
{
    [Route("[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        IBookLogic logic;

        public BookController(IBookLogic logic)
        {
            this.logic = logic;
        }

        // GET: api/<BookController>
        [HttpGet]
        public IEnumerable<Book> ReadAll()
        {
            return logic.ReadAll();
        }

        // GET api/<BookController>/5
        [HttpGet("{id}")]
        public Book Read(int id)
        {
            return logic.Read(id);
        }

        // POST api/<BookController>
        [HttpPost]
        public void Create([FromBody] Book value)
        {
            logic.Create(value);
        }

        // PUT api/<BookController>/5
        [HttpPut]
        public void Update([FromBody] Book value)
        {
            logic.Update(value);
        }

        // DELETE api/<BookController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            logic.Delete(id);
        }
    }
}
