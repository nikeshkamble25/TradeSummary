using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeSummaryReport.Dto
{
    public class TradeSummary
    {
        public float QuantitySum { get; set; }
        public float PriceAvg { get; set; }
        public string BloombergId { get; set; }
        public string TransactionCode { get; set; }
        public DateTime? TradeDate { get; set; }
    }
}
