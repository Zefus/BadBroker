using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BadBroker.BusinessLogic.Interfaces;
using BadBroker.BusinessLogic.ModelsDTO;
using BadBroker.WebService.Validation;
using BadBroker.BusinessLogic.Exceptions;

namespace BadBroker.WebService.Controllers
{
    public class HomeController : Controller
    {
        public IModelValidator _modelValidator;
        private ITradeService _tradeService;

        public HomeController(ITradeService tradeService, IModelValidator modelValidator)
        {
            _tradeService = tradeService;
            _modelValidator = modelValidator;
        }

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
                if (_modelValidator.Validate(inputDTO))
                {
                    if (_tradeService == null)
                        throw new TradeServiceException("Property '_tradeService' is null");

                    OutputDTO result = await _tradeService.MakeTrade(inputDTO);
                    return Json(new { Success = true, result});
                }
                else
                {
                    return Json(new { Success = false, redirectUrl = "/home/internalerror" });
                }
            }
            catch (TradeServiceException ex)
            {
                return Json(new { Success = false, redirectUrl =  "/home/internalerror"});
            }
        }

        public IActionResult InternalError()
        {
            return View();
        }
    }
}
