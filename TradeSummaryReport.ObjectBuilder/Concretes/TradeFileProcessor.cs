using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using TradeSummaryReport.Dto;
using TradeSummaryReport.FileDataBuilder.Contracts;
using TradeSummaryReport.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TradeSummaryReport.FileDataBuilder.Concretes
{
    public class TradeFileProcessor : IFileProcessor<List<TradeDto>>
    {
        protected string _fileFilter = "Trade*.xml";

        protected List<TradeDto> _tradeDtoList = null;
        protected List<TradesDto> _tradesDtoList = null;

        protected TradesDto _tradeDto = null;
        protected XmlSerializer _xmlSerializer = null;
        protected XmlReader _xmlReader = null;
        protected FileStream _fileStream = null;

        protected String _tradesValue = "Trades";
        protected String _tradeValue = "Trade";
        protected String _securityValue = "Security";

        protected String _tradingAccount = "TradingAccount";
        protected String _transactionCode = "TransactionCode";
        protected String _quantity = "Quantity";
        protected String _tradeCurrency = "TradeCurrency";
        protected String _settlementCurrency = "SettlementCurrency";
        protected String _price = "Price";
        protected String _execBroker = "ExecBroker";
        protected String _security = "Security";

        protected String _tradeDate = "TradeDate";
        protected String _settleDate = "SettleDate";
        protected String _actualDate = "ActualDate";
       
        protected String _id = "Id";
        protected String _code = "Code";

        protected String _stringValue = "String";

        public TradeFileProcessor()
        {
            _tradeDto = new TradesDto();
            _tradeDtoList = new List<TradeDto>();
            _tradesDtoList = new List<TradesDto>();
        }
        public virtual List<TradeDto> OutputObject
        {
            get
            {
                return _tradeDtoList;
            }
        }
        public string Path { get; set; }
        public virtual void ProcessObject()
        {
            _tradesDtoList.AsParallel().ForAll(obj=> {
                lock (this)
                {
                    _tradeDtoList.AddRange(obj.Trade);
                }                
            });
        }
        public virtual void ReadFile()
        {
            _xmlSerializer = new XmlSerializer(typeof(TradesDto), GetXmlAttributeOverrides());
            var fileList = ReadTrades(Path);
            Parallel.ForEach(fileList, path =>
            {
                lock (this)
                {
                    using (_fileStream = new FileStream(path, FileMode.Open))
                    { 
                        using (_xmlReader = XmlReader.Create(_fileStream))
                        {
                            _tradeDto = (TradesDto)_xmlSerializer.Deserialize(_xmlReader);
                            _tradesDtoList.Add(_tradeDto);
                        }
                    }
                }
            });
        }
        protected virtual XmlAttributeOverrides GetXmlAttributeOverrides()
        {
            var overrides = new XmlAttributeOverrides();

            OverrideRoot(overrides);
            OverrideTradeElement(overrides);
            OverrideXmlAttribute(overrides, typeof(TradesDto), _tradeValue.ToCamelCase(), _tradeValue);
            OverrideSecurityElement(overrides);
            OverrideIgnore(overrides);
            return overrides;
        }
        protected virtual void OverrideIgnore(XmlAttributeOverrides overrides)
        {
            overrides.Add(typeof(HistoricalPriceDto), _tradeDate, new XmlAttributes { XmlIgnore = true });
            overrides.Add(typeof(HistoricalPriceDto), _settleDate, new XmlAttributes { XmlIgnore = true });
            overrides.Add(typeof(HistoricalPriceDto), _actualDate, new XmlAttributes { XmlIgnore = true });
        }
        private void OverrideRoot(XmlAttributeOverrides overrides)
        {
            OverrideXmlAttribute(overrides, typeof(TradesDto), _tradesValue.ToCamelCase());
            OverrideXmlAttribute(overrides, typeof(TradeDto), _tradeValue.ToCamelCase());
            OverrideXmlAttribute(overrides, typeof(TradeSecurityDto), _securityValue.ToCamelCase());
        }
        private void OverrideTradeElement(XmlAttributeOverrides overrides)
        {
            OverrideXmlAttribute(overrides, typeof(TradeDto), _tradingAccount.ToCamelCase(), _tradingAccount);
            OverrideXmlAttribute(overrides, typeof(TradeDto), _transactionCode.ToCamelCase(), _transactionCode);
            OverrideXmlAttribute(overrides, typeof(TradeDto), _quantity.ToCamelCase(), _quantity);
            OverrideXmlAttribute(overrides, typeof(TradeDto), _tradeCurrency.ToCamelCase(), _tradeCurrency);
            OverrideXmlAttribute(overrides, typeof(TradeDto), _settlementCurrency.ToCamelCase(), _settlementCurrency);
            OverrideXmlAttribute(overrides, typeof(TradeDto), _price.ToCamelCase(), _price);
            OverrideXmlAttribute(overrides, typeof(TradeDto), _execBroker.ToCamelCase(), _execBroker);
            OverrideXmlAttribute(overrides, typeof(TradeDto), _security.ToCamelCase(), _security);

            OverrideXmlAttribute(overrides, typeof(TradeDto), _tradeDate.ToCamelCase(), _tradeDate + _stringValue);
            OverrideXmlAttribute(overrides, typeof(TradeDto), _settleDate.ToCamelCase(), _settleDate + _stringValue);
            OverrideXmlAttribute(overrides, typeof(TradeDto), _actualDate.ToCamelCase(), _actualDate + _stringValue);
        }
        private void OverrideSecurityElement(XmlAttributeOverrides overrides)
        {
            OverrideXmlAttribute(overrides, typeof(TradeSecurityDto), _id.ToCamelCase(), _id);
            OverrideXmlAttribute(overrides, typeof(TradeSecurityDto), _code.ToCamelCase(), _code);
        }
        protected void OverrideXmlAttribute(XmlAttributeOverrides overrides, Type type, string elementName, string memeberName)
        {
            XmlAttributes xmlAttr = new XmlAttributes();
            xmlAttr.XmlElements.Add(new XmlElementAttribute() { ElementName = elementName });
            overrides.Add(type, memeberName, xmlAttr);
        }
        protected void OverrideXmlAttribute(XmlAttributeOverrides overrides, Type type, string elementName)
        {
            overrides.Add(type, new XmlAttributes
            {
                XmlRoot = new XmlRootAttribute(elementName)
                {
                    IsNullable = true,
                    ElementName = elementName
                }
            });
        }
        protected String[] ReadTrades(string parentPath)
        {
            String[] tradeFiles = Directory.GetFiles(parentPath, _fileFilter, SearchOption.AllDirectories)
                         .ToArray();
            return tradeFiles;
        }
    }
}
