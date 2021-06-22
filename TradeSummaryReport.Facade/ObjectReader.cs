using System.Collections.Generic;
using System.Linq;
using TradeSummaryReport.Dto;
using TradeSummaryReport.Facade.SubSystems;
using TradeSummaryReport.FileDataBuilder.Contracts;
using TradeSummaryReport.ObjectBuilder.Builder.Contracts;
using Unity;

namespace TradeSummaryReport.Facade
{
    public class ObjectReader
    {
        TradeObject _tradeObject = null;
        SecurityObject _securityObject = null;
        public ObjectReader()
        {
            _tradeObject = new TradeObject();
            _securityObject = new SecurityObject();
        }
        public List<TradeSummary> GetAggregatedTrades(string tradePath, string securityPath)
        {
            var tradeList = _tradeObject.GetTrades(tradePath);
            var securities = _securityObject.GetSecurities(securityPath);
            var aggregatedTrade = tradeList.Join(
                    securities.Securities,
                    objTrade => objTrade.Security.Id,
                    objSecurity => objSecurity.Id,
                    (objTrade, objSecurity) =>
                        new
                        {
                            objSecurity.BloombergId,
                            objTrade.TransactionCode,
                            objTrade.TradeDate,
                            objTrade.Quantity,
                            objTrade.Price
                        }
                    )
                    .GroupBy(objGroup => new
                    {
                        objGroup.BloombergId,
                        objGroup.TransactionCode,
                        objGroup.TradeDate
                    }).
                    Select(objGroup => new TradeSummary
                    {
                        QuantitySum = objGroup.Sum(obj => obj.Quantity),
                        PriceAvg = objGroup.Sum(obj => obj.Price),
                        BloombergId = objGroup.Key.BloombergId,
                        TransactionCode = objGroup.Key.TransactionCode,
                        TradeDate = objGroup.Key.TradeDate
                    })
                    .ToList();
            return aggregatedTrade;
        }        
        public List<TradesDto> GetImpureTrades(string tradePath, string securityPath)
        {
            var tradeList = _tradeObject.GetTradesFiles(tradePath);
            var securities = _securityObject.GetSecurities(securityPath);
            var securityIds = securities.Securities.Select(objSecurity => objSecurity.Id).ToArray();
            var aggregatedTrade = tradeList
                .Where(objTradeList =>
                        objTradeList.Trade.Any(
                                objSecurity => !securityIds.Contains<int>(objSecurity.Security.Id)
                                )
                        )
                .Select(obj => new TradesDto {
                    Path = obj.Path,
                    Trade = obj.Trade.Where(objSecurity => !securityIds.Contains<int>(objSecurity.Security.Id)).ToList()
                }).ToList();
            return aggregatedTrade;
        }
    }
}
