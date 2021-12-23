using EBroker.Models;
using EBroker.Services.Interfaces;
using EBroker.Utils.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace EBroker.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TraderController : ControllerBase
    {

        private readonly ILogger<TraderController> _logger;

        private readonly ITradeService _tradeService;

        private readonly ITradeHelperWrapper _tradeHelperWrapper;

        public TraderController(ILogger<TraderController> logger, ITradeService tradeService, ITradeHelperWrapper tradeHelperWrapper)
        {
            _logger = logger;
            _tradeService = tradeService;
            _tradeHelperWrapper = tradeHelperWrapper;
        }

        [HttpPut("addFund")]
        public async Task<IActionResult> AddFund(TraderFund traderFundRequest)
        {
            if (traderFundRequest.Id > 0 && traderFundRequest.Funds > 0)
            {
                var result = await _tradeService.AddFunds(traderFundRequest);
                if (result != "Invalid Trader")
                    return Ok(result);
                return NotFound(result);
            }
            return BadRequest("TraderId & Funds should be greater than 0");
        }

        /// <summary>
        /// Sell Equity
        /// </summary>
        /// <param name="traderTransaction"></param>
        [HttpPost("sellEquity")]
        public async Task<IActionResult> SellEquityTransaction([FromBody] TraderTransaction traderTransactionRequest)
        {
            if (traderTransactionRequest.TransactionUnits > 0 && traderTransactionRequest.TraderId > 0 && traderTransactionRequest.EquityId > 0)
            {
                if (_tradeHelperWrapper.IsValidTransactionTime())
                {
                    var result = await _tradeService.SellEquity(traderTransactionRequest);
                    if (result == null)
                        return Ok();
                    return NotFound(result);
                }
                else
                {
                    return BadRequest("Transaction should be placed between 9AM -3PM & Mon -Fri");
                }
            }
            return BadRequest("EquityId ,TraderId & Transaction Units should be greater than 0");
        }

        /// <summary>
        /// Buy Equity
        /// </summary>
        /// <param name="traderTransaction"></param>
        [HttpPost("buyEquity")]
        public async Task<IActionResult> BuyEquityTransaction([FromBody] TraderTransaction traderTransactionRequest)
        {
            if (traderTransactionRequest.TransactionUnits > 0 && traderTransactionRequest.TraderId > 0 && traderTransactionRequest.EquityId > 0)
            {
                if (_tradeHelperWrapper.IsValidTransactionTime())
                {
                    var result = await _tradeService.BuyEquity(traderTransactionRequest);
                    if (result == null)
                        return Ok();
                    return NotFound(result);
                }
                else
                {
                    return BadRequest("Transaction should be placed between 9AM -3PM & Mon -Fri");
                }
            }
            return BadRequest("EquityId ,TraderId & Transaction Units should be greater than 0");
        }

    }
}
