using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using UHRRJ1_HFT_2022232.Endpoint.Services;
using UHRRJ1_HFT_2022232.Logic.Interfaces;
using UHRRJ1_HFT_2022232.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UHRRJ1_HFT_2022232.Endpoint.Controllers.models
{
    [Route("[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        IHubContext<SignalRHub> hub;
        IAuthorLogic logic;
        public AuthorController(IAuthorLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }

        [HttpGet]
        public IEnumerable<Author> ReadAll()
        {
            return logic.ReadAll();
        }

        [HttpGet("{id}")]
        public Author Read(int id)
        {
            return logic.Read(id);
        }

        [HttpPost]
        public void Create([FromBody] Author value)
        {
            logic.Create(value);
            hub.Clients.All.SendAsync("AuthorCreated", value);
        }

        [HttpPut]
        public void Update([FromBody] Author value)
        {
            logic.Update(value);
            hub.Clients.All.SendAsync("AuthorUpdated", value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            logic.Delete(id);
            hub.Clients.All.SendAsync("AuthorDeleted", logic.Read(id));
        }
    }
}
