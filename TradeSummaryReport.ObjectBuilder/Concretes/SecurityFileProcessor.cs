using System;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;
using TradeSummaryReport.Dto;
using TradeSummaryReport.FileDataBuilder.Contracts;

namespace TradeSummaryReport.FileDataBuilder.Concretes
{
    public class SecurityFileProcessor : IFileProcessor<SecuritiesDto>
    {
        protected SecuritiesDto _securityDto = null;
        protected XmlSerializer _xmlSerializer = null;
        protected XmlReader _xmlReader = null;
        protected FileStream _fileStream = null;

        protected String _securitiesValue = "Securities";
        protected String _securityValue = "Security";
        protected String _historicalPriceValue = "HistoricalPrice";
        protected String _historicalPricesValue = "HistoricalPrices";
        protected String _effectiveDateValue = "EffectiveDate";
        protected String _effectiveDateStringValue = "EffectiveDateString";

        //Needs to move in config file
        //protected String _filePath = @"C:\Users\nikes\OneDrive\Desktop\Engineer Code Test\Backend\Securities.xml";
        public SecurityFileProcessor()
        {
            _securityDto = new SecuritiesDto();
        }
        public SecuritiesDto OutputObject
        {
            get
            {
                return _securityDto;
            }
        }
        public string Path { get; set; }
        public void ProcessObject()
        {
            _securityDto = (SecuritiesDto)_xmlSerializer.Deserialize(_xmlReader);
            _fileStream.Close();
            _fileStream.DisposeAsync();
        }
        public void ReadFile()
        {
            _xmlSerializer = new XmlSerializer(typeof(SecuritiesDto), GetXmlAttributeOverrides());
            _fileStream = new FileStream(Path, FileMode.Open);
            _xmlReader = XmlReader.Create(_fileStream);
        }
        protected virtual XmlAttributeOverrides GetXmlAttributeOverrides()
        {
            var overrides = new XmlAttributeOverrides();
            OverrideRoot(overrides);
            OverrideElements(overrides);
            OverrideIgnore(overrides);
            return overrides;
        }
        private void OverrideIgnore(XmlAttributeOverrides overrides)
        {
            overrides.Add(typeof(HistoricalPriceDto), _effectiveDateValue, new XmlAttributes { XmlIgnore = true });
        }
        private void OverrideRoot(XmlAttributeOverrides overrides)
        {
            OverrideXmlAttribute(overrides, typeof(SecuritiesDto), _securitiesValue);
            OverrideXmlAttribute(overrides, typeof(HistoricalPriceDto), _historicalPriceValue);
            OverrideXmlAttribute(overrides, typeof(SecurityDto), _securityValue);
            OverrideXmlAttribute(overrides, typeof(HistoricalPricesDto), _historicalPricesValue);
        }
        private void OverrideElements(XmlAttributeOverrides overrides)
        {
            OverrideXmlAttribute(overrides, typeof(SecuritiesDto), _securityValue, _securitiesValue);
            OverrideXmlAttribute(overrides, typeof(HistoricalPricesDto), _historicalPriceValue, _historicalPriceValue);
            OverrideXmlAttribute(overrides, typeof(HistoricalPriceDto), _effectiveDateValue, _effectiveDateStringValue);
        }
        protected virtual void OverrideXmlAttribute(XmlAttributeOverrides overrides, Type type, string elementName, string memeberName)
        {
            XmlAttributes xmlAttr = new XmlAttributes();
            xmlAttr.XmlElements.Add(new XmlElementAttribute() { ElementName = elementName });
            overrides.Add(type, memeberName, xmlAttr);
        }
        protected virtual void OverrideXmlAttribute(XmlAttributeOverrides overrides, Type type, string elementName)
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
    }
}
