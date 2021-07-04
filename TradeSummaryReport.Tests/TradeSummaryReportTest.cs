using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TradeSummaryReport.Dto;
using TradeSummaryReport.Facade;
using Xunit;

namespace TradeSummaryReport.Tests
{
    public class TradeSummaryReportTest
    {
        List<TradeSummary> tradeAggreList;
        public TradeSummaryReportTest()
        {
            string path = Directory.GetCurrentDirectory();
            ObjectReader objectReader = new ObjectReader();
            tradeAggreList = objectReader.GetAggregatedTrades(path + @"/TradeFiles", path + @"/TradeFiles/Securities.xml");
        }
        [Fact(DisplayName ="To Identify Count of Trades")]
        public void CheckTradeReportObject()
        {
            Assert.Equal(Convert.ToInt32(tradeAggreList.Count), Convert.ToInt32(3));
        }
        //[Fact(DisplayName = "To Identify Avg of Trades")]
        //public void CheckTradeReportAvg()
        //{
        //    Assert.Equal(Convert.ToInt32(tradeAggreList.Where(obj=>obj.BloombergId== "AKM4JZ7").FirstOrDefault().PriceAvg), Convert.ToInt32(3600));
        //}
    }
}
