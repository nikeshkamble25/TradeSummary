using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeSummaryReport.Dto
{
	public class TradeDto
	{
		protected string _tradeDateString = string.Empty;
		protected string _settleDateString = string.Empty;
		protected string _actualDateString = string.Empty;
		public string TradingAccount { get; set; }
		public DateTime? TradeDate { get; set; }
		public DateTime? SettleDate { get; set; }
		public DateTime? ActualDate { get; set; }
		public string TransactionCode { get; set; }
		public float Quantity { get; set; }
		public string TradeCurrency { get; set; }
		public string SettlementCurrency { get; set; }
		public float Price { get; set; }
		public string ExecBroker { get; set; }
		public TradeSecurityDto Security { get; set; }
		public virtual String TradeDateString
		{
			get
			{
				return _tradeDateString;
			}
			set
			{
				TradeDate = DateTime.ParseExact(value, "yyyyMMdd", null);
				_tradeDateString = value;
			}
		}
		public virtual String SettleDateString
		{
			get
			{
				return _settleDateString;
			}
			set
			{
				SettleDate = DateTime.ParseExact(value, "yyyyMMdd", null);
				_settleDateString = value;
			}
		}
		public virtual String ActualDateString
		{
			get
			{
				return _actualDateString;
			}
			set
			{
				ActualDate = DateTime.ParseExact(value, "yyyyMMdd", null);
				_actualDateString = value;
			}
		}
	}
}
