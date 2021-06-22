using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeSummaryReport.Dto
{
    public class TradesDto
    {
        public String Path { get; set; }
        public List<TradeDto> Trade { get; set; }
    }
}
