using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using UHRRJ1_HFT_2022232.Endpoint.Services;
using UHRRJ1_HFT_2022232.Logic.Interfaces;
using UHRRJ1_HFT_2022232.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UHRRJ1_HFT_2022232.Endpoint.Controllers.models
{
    [Route("[controller]")]
    [ApiController]
    public class BookStoreController : ControllerBase
    {
        IHubContext<SignalRHub> hub;
        IBookStoreLogic logic;
        public BookStoreController(IBookStoreLogic logic)
        {
            this.logic = logic;
        }

        [HttpGet]
        public IEnumerable<BookStore> ReadAll()
        {
            return logic.ReadAll();
        }

        [HttpGet("{id}")]
        public BookStore Read(int id)
        {
            return logic.Read(id);
        }

        [HttpPost]
        public void Create([FromBody] BookStore value)
        {
            logic.Create(value);
            hub.Clients.All.SendAsync("BookStoreCreated", value);
        }

        [HttpPut]
        public void Update([FromBody] BookStore value)
        {
            logic.Update(value);
            hub.Clients.All.SendAsync("BookStoreUpdated", value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            logic.Delete(id);
            hub.Clients.All.SendAsync("BookStoreDeleted", logic.Read(id));
        }
    }
}
