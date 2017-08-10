using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BadBroker.BusinessLogic.DataTransferObjects;

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
            if (model != null)
            {
                int i = 0;
                return Json("Success");
            }
            else
            {
                return Json("An Error Has occoured");
            }
        }
    }
}