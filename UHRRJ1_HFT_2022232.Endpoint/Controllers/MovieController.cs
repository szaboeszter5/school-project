using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using UHRRJ1_HFT_2022232.Logic.Interfaces;
using UHRRJ1_HFT_2022232.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UHRRJ1_HFT_2022232.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        IMovieLogic logic;

        public MovieController(IMovieLogic logic)
        {
            this.logic = logic;
        }

        // GET: api/<MovieController>
        [HttpGet]
        public IEnumerable<Movie> ReadAll()
        {
            return this.logic.ReadAll(); 
        }

        // GET api/<MovieController>/5
        [HttpGet("{id}")]
        public Movie Read(int id)
        {
            return logic.Read(id);
        }

        // POST api/<MovieController>
        [HttpPost]
        public void Create([FromBody] Movie value)
        {
            logic.Create(value);
        }

        // PUT api/<MovieController>/5
        [HttpPut]
        public void Update([FromBody] Movie value)
        {
            logic.Update(value);
        }

        // DELETE api/<MovieController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            logic.Delete(id);
        }
    }
}
