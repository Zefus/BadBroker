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
        public ITradeService _tradeService { get; set; }
        public HomeController(ITradeService tradeService)
        {
            _tradeService = tradeService;
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
                ValidationModel validationModel = new ValidationModel();
                if (validationModel.Validate(inputDTO))
                {
                    OutputDTO result = await _tradeService.MakeTrade(inputDTO);
                    return Json(new { Success = true, result});
                }
                else
                {
                    return BadRequest();
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
