using EBroker.Data.Repositories.Interfaces;
using EBroker.Models;
using EBroker.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace EBroker.Services
{
    public class TradeService : ITradeService
    {
        private readonly ITradeRepository _tradeRepository;
        public TradeService(ITradeRepository tradeRepository)
        {
            _tradeRepository = tradeRepository;
        }

        public async Task<string> BuyEquity(TraderTransaction traderTransaction)
        {
            var equityInfoEntity = await _tradeRepository.GetEquityById(traderTransaction.EquityId);
            if (equityInfoEntity == null)
            {
                return "Invalid Equity";
            }
            var traderInfoEntity = await _tradeRepository.GetTraderById(traderTransaction.TraderId);
            if (traderInfoEntity == null)
            {
                return "Invalid Trader";
            }
            var fundLeftAfterTransaction = traderInfoEntity.Funds - equityInfoEntity.UnitPrice * traderTransaction.TransactionUnits;
            if (fundLeftAfterTransaction < 0)
            {
                return "InSufficient Funds";
            }
            await TradeHoldingOps(traderTransaction);
            traderInfoEntity.Funds = fundLeftAfterTransaction;
            await _tradeRepository.UpdateTrader(traderInfoEntity);
            await TradeTransactionOps(traderTransaction, TradeAction.Buy);
            await _tradeRepository.Complete();
            return null;
        }

        private async Task TradeHoldingOps(TraderTransaction traderTransaction)
        {
            var traderHoldings = await _tradeRepository.GetTraderHoldingsByEquityTraderId(traderTransaction.EquityId, traderTransaction.TraderId);
            var traderHoldingsEntity = new Data.Entities.TraderHolding()
            {
                EquityId = traderTransaction.EquityId,
                TraderId = traderTransaction.TraderId,
                UnitHoldings = traderTransaction.TransactionUnits
            };
            if (traderHoldings == null)
            {
                await _tradeRepository.AddTraderHoldings(traderHoldingsEntity);
            }
            else
            {
                traderHoldingsEntity.UnitHoldings = traderHoldings.UnitHoldings + traderTransaction.TransactionUnits;
                await _tradeRepository.UpdateTraderHoldings(traderHoldingsEntity);
            }
        }

        public async Task<string> SellEquity(TraderTransaction traderTransaction)
        {
            var equityInfoEntity = await _tradeRepository.GetEquityById(traderTransaction.EquityId);
            if (equityInfoEntity == null)
            {
                return "Invalid Equity";
            }
            var traderInfoEntity = await _tradeRepository.GetTraderById(traderTransaction.TraderId);
            if (traderInfoEntity == null)
            {
                return "Invalid Trader";
            }
            var traderHoldings = await _tradeRepository.GetTraderHoldingsByEquityTraderId(traderTransaction.EquityId, traderTransaction.TraderId);
            if (traderHoldings == null || traderHoldings.UnitHoldings < traderTransaction.TransactionUnits)
            {
                return "Can't sell as transaction units are less than holding units";
            }
            var soldEquityAmount = equityInfoEntity.UnitPrice * traderTransaction.TransactionUnits;
            soldEquityAmount -= Math.Max((soldEquityAmount * 0.05 / 100), 20.00);
            traderInfoEntity.Funds += soldEquityAmount;
            await _tradeRepository.UpdateTrader(traderInfoEntity);
            var traderHoldingsEntity = new Data.Entities.TraderHolding()
            {
                EquityId = traderTransaction.EquityId,
                TraderId = traderTransaction.TraderId,
                UnitHoldings = traderHoldings.UnitHoldings - traderTransaction.TransactionUnits
            };
            await _tradeRepository.UpdateTraderHoldings(traderHoldingsEntity);
            await TradeTransactionOps(traderTransaction, TradeAction.Sell);
            await _tradeRepository.Complete();
            return null;
        }

        private async Task TradeTransactionOps(TraderTransaction traderTransaction, TradeAction tradeActionType)
        {
            EBroker.Data.Entities.TraderTransaction traderTransactionEntity = new EBroker.Data.Entities.TraderTransaction();
            traderTransactionEntity.EquityId = traderTransaction.EquityId;
            traderTransactionEntity.TraderId = traderTransaction.TraderId;
            traderTransactionEntity.TransactionUnits = traderTransaction.TransactionUnits;
            traderTransactionEntity.TraderAction = tradeActionType;
            await _tradeRepository.AddTraderTransaction(traderTransactionEntity);
        }

        public async Task<string> AddFunds(TraderFund traderFundRequest)
        {
            var traderInfoEntity = await _tradeRepository.GetTraderById(traderFundRequest.Id);
            if (traderInfoEntity == null)
            {
                return "Invalid Trader";
            }
            traderInfoEntity.Funds = traderInfoEntity.Funds + traderFundRequest.Funds;
            if (traderInfoEntity.Funds > 100000)
            {
                var above1LakhFund = traderInfoEntity.Funds - 100000;
                traderInfoEntity.Funds = above1LakhFund * 0.95 + 100000;
            }
            await _tradeRepository.UpdateTrader(traderInfoEntity);
            await _tradeRepository.Complete();
            return null;
        }
    }
}
