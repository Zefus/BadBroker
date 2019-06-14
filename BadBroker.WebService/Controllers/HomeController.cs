using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BadBroker.WebService.Models;
using BadBroker.BusinessLogic.Services;
using BadBroker.BusinessLogic.ModelsDTO;
using BadBroker.WebService.Validation;

namespace BadBroker.WebService.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index([FromBody] InputDTO inputDTO)
        {
            try
            {
                ValidationModel validationModel = new ValidationModel();
                if (validationModel.Validate(inputDTO))
                {
                    TradeService tradeService = new TradeService();
                    OutputDTO result = await tradeService.MakeTrade(inputDTO);
                    return Json(result);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                throw new NotImplementedException();
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
