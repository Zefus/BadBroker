using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BadBroker.WebService.Models;
using BadBroker.BusinessLogic.Services;
using BadBroker.BusinessLogic.ModelsDTO;

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
        public async Task<IActionResult> Index(InputDTO inputDTO)
        {
            try
            {
                TradeService tradeService = new TradeService();
                OutputDTO result = await tradeService.MakeTrade(inputDTO);
                return Json(result);
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
