using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using UHRRJ1_HFT_2022232.Logic.Interfaces;
using UHRRJ1_HFT_2022232.Logic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UHRRJ1_HFT_2022232.Endpoint.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class StatController : ControllerBase
    {
        IBookLogic logic;

        public StatController(IBookLogic logic)
        {
            this.logic = logic;
        }

        [HttpGet("{year}")]
        public double? AverageRatePerYear(int year)
        {
            return this.logic.GetAverageRatePerYear(year);
        }

        [HttpGet]
        public IEnumerable<BookLogic.YearInfo> YearStatistics(int year)
        {
            return this.logic.YearStatistics();
        }
    }
}
