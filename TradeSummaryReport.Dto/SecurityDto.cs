using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeSummaryReport.Dto;

namespace TradeSummaryReport.Dto
{
    public class SecurityDto
    {
        public int Id { get; set; }
        public string BloombergId { get; set; }
        public string IssueCountry { get; set; }
        public HistoricalPricesDto HistoricalPrices { get; set; }
    }
}
