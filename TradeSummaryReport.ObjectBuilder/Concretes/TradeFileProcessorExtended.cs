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
    public class TradeFileProcessorExtended : TradeFileProcessor, IFileProcessor<List<TradesDto>>
    {
        protected string _path = "Path";
        List<TradesDto> IFileProcessor<List<TradesDto>>.OutputObject 
        {
            get 
            { 
                return _tradesDtoList; 
            } 
        }
        protected override void OverrideIgnore(XmlAttributeOverrides overrides)
        {
            base.OverrideIgnore(overrides);
            overrides.Add(typeof(HistoricalPriceDto), _path, new XmlAttributes { XmlIgnore = true });
        }
        public override void ReadFile()
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
                            _tradeDto.Path = path;
                            _tradesDtoList.Add(_tradeDto);
                        }
                    }
                }
            });
        }
    }
}
