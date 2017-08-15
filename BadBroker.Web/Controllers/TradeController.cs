using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BadBroker.BusinessLogic.DataTransferObjects;
using BadBroker.BusinessLogic.Services;
using Newtonsoft.Json;

namespace BadBroker.Web.Controllers
{
    public class TradeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public string Index(InputDTO model)
        {
            TradeService tradeService = new TradeService();
            var result = tradeService.MakeTrade(model);
            return JsonConvert.SerializeObject(result);
        }
    }
}