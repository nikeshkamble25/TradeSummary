using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeSummaryReport.Dto
{
    public class HistoricalPriceDto
    {
        protected string _effectiveDateString = string.Empty;
        public float Bid { get; set; }
        public float Ask { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public virtual String EffectiveDateString 
        {
            get
            {
                return _effectiveDateString;
            }
            set
            {
                EffectiveDate = DateTime.ParseExact(value, "yyyyMMdd", null);
                _effectiveDateString = value;
            }
        }
    }
}
