using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BadBroker.BusinessLogic.DataTransferObjects;
using BadBroker.BusinessLogic.Services;

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
        public ActionResult Index(InputDTO model)
        {
            int ii = 0;
            TradeService tradeService = new TradeService();
            var result = tradeService.MakeTrade(model);
            int i = 0;
            return View();
        }
    }
}