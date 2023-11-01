using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using UHRRJ1_HFT_2022232.Endpoint.Services;
using UHRRJ1_HFT_2022232.Logic;
using UHRRJ1_HFT_2022232.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UHRRJ1_HFT_2022232.Endpoint.Controllers.models
{
    [Route("[controller]")]
    [ApiController]
    public class ReaderController : ControllerBase
    {
        IReaderLogic logic;
        IHubContext<SignalRHub> hub;

        public ReaderController(IReaderLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }

        [HttpGet]
        public IEnumerable<Reader> ReadAll()
        {
            return logic.ReadAll();
        }

        [HttpGet("{id}")]
        public Reader Read(int id)
        {
            return logic.Read(id);
        }

        [HttpPost]
        public void Create([FromBody] Reader value)
        {
            logic.Create(value);
            this.hub.Clients.All.SendAsync("ReaderCreated", value);
        }

        [HttpPut]
        public void Update([FromBody] Reader value)
        {
            logic.Update(value);
            this.hub.Clients.All.SendAsync("ReaderUpdated", value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var deleted = this.logic.Read(id);
            logic.Delete(id);
            this.hub.Clients.All.SendAsync("ReaderDeleted", deleted);
        }
    }
}
