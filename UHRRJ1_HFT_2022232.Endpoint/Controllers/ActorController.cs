using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using UHRRJ1_HFT_2022232.Logic;
using UHRRJ1_HFT_2022232.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UHRRJ1_HFT_2022232.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ActorController : ControllerBase
    {
        IActorLogic logic;
        public ActorController(IActorLogic logic)
        {
            this.logic = logic;
        }

        [HttpGet]
        public IEnumerable<Actor> ReadAll()
        {
            return logic.ReadAll();
        }

        [HttpGet("{id}")]
        public Actor Read(int id)
        {
            return logic.Read(id);
        }

        [HttpPost]
        public void Create([FromBody] Actor value)
        {
            logic.Create(value);
        }

        [HttpPut]
        public void Put([FromBody] Actor value)
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
