using System.Collections.Generic;
using TradeSummaryReport.Dto;
using TradeSummaryReport.FileDataBuilder.Contracts;
using TradeSummaryReport.ObjectBuilder.Builder.Contracts;
using Unity;

namespace TradeSummaryReport.Facade.SubSystems
{
    internal class TradeObject
    {
        internal List<TradeDto> GetTrades(string path)
        {
            IFileProcessor<List<TradeDto>> fileProcessor = UnityClient.ContainerObject.GetContainer()
                .Resolve<IFileProcessor<List<TradeDto>>>();
            IDirector<IFileProcessor<List<TradeDto>>> objectProcessor = UnityClient.ContainerObject.GetContainer()
                .Resolve<IDirector<IFileProcessor<List<TradeDto>>>>();
            fileProcessor.Path = path;
            objectProcessor.ConstructObject(fileProcessor);
            return fileProcessor.OutputObject;
        }
        internal List<TradesDto> GetTradesFiles(string path)
        {
            IFileProcessor<List<TradesDto>> fileProcessor = UnityClient.ContainerObject.GetContainer()
                .Resolve<IFileProcessor<List<TradesDto>>>();
            IDirector<IFileProcessor<List<TradesDto>>> objectProcessor = UnityClient.ContainerObject.GetContainer()
                .Resolve<IDirector<IFileProcessor<List<TradesDto>>>>();
            fileProcessor.Path = path;
            objectProcessor.ConstructObject(fileProcessor);
            var listOfTradeFile = fileProcessor.OutputObject;
            return listOfTradeFile;
        }
    }
}
