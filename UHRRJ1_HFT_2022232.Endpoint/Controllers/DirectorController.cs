using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using UHRRJ1_HFT_2022232.Logic.Interfaces;
using UHRRJ1_HFT_2022232.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UHRRJ1_HFT_2022232.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DirectorController : ControllerBase
    {
        IDirectorLogic logic;
        public DirectorController(IDirectorLogic logic)
        {
            this.logic = logic;
        }

        [HttpGet]
        public IEnumerable<Director> ReadAll()
        {
            return logic.ReadAll();
        }

        [HttpGet("{id}")]
        public Director Read(int id)
        {
            return logic.Read(id);
        }

        [HttpPost]
        public void Create([FromBody] Director value)
        {
            logic.Create(value);
        }

        [HttpPut]
        public void Update([FromBody] Director value)
        {
            logic.Update(value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            logic.Delete(id);
        }
    }
}
