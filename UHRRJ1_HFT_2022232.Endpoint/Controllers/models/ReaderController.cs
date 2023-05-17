using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
        public ReaderController(IReaderLogic logic)
        {
            this.logic = logic;
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
        }

        [HttpPut]
        public void Update([FromBody] Reader value)
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
