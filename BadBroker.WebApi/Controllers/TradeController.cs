using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BadBroker.BusinessLogic.ModelsDTO;
using BadBroker.BusinessLogic.Services;
using BadBroker.WebApi.Errors;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BadBroker.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class TradeController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] InputDTO inputDTO)
        {
            try
            {
                TradeService tradeService = new TradeService();

                OutputDTO outputDTO = await tradeService.MakeTrade(inputDTO.StartDate, inputDTO.EndDate);

                if (outputDTO == null)
                {
                    return Json(new Error("result is null!", 500));
                }

                return Json(outputDTO);
            }
            catch (Exception ex)
            {
                throw new NotImplementedException();
            }
        }
    }
}
