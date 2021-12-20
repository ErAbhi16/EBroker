using EBroker.Models;
using EBroker.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EBroker.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TraderController : ControllerBase
    {

        private readonly ILogger<TraderController> _logger;

        private readonly ITradeService _tradeService;

        public TraderController(ILogger<TraderController> logger,ITradeService tradeService)
        {
            _logger = logger;
            _tradeService = tradeService;
        }

        [HttpPut("/addFund")]
        public void AddFund(TraderFund traderFundRequest)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="traderTransaction"></param>
        [HttpPost("/transaction")]
        public void EquityTransaction(TraderTransaction traderTransactionRequest)
        {

        }

    }
}
