using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BadBroker.BusinessLogic.ModelsDTO;
using BadBroker.BusinessLogic.Services;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BadBroker.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class TradeController : Controller
    {
        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] InputDTO inputDTO)
        {
            try
            {
                TradeService tradeService = new TradeService();

                OutputDTO outputDTO = await tradeService.MakeTrade(inputDTO.StartDate, inputDTO.EndDate);

                return Json(outputDTO);
            }
            catch (Exception ex)
            {
                throw new NotImplementedException();
            }
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
