using System.Collections.Generic;
using TradeSummaryReport.Dto;
using TradeSummaryReport.FileDataBuilder.Concretes;
using TradeSummaryReport.FileDataBuilder.Contracts;
using TradeSummaryReport.ObjectBuilder.Builder.Concretes;
using TradeSummaryReport.ObjectBuilder.Builder.Contracts;
using Unity;

namespace TradeSummaryReport
{
    public class UnityClient
    {
        private static UnityClient _unityClient = null;
        private static IUnityContainer _container = null;
        private UnityClient()
        {            
        }
        static UnityClient() 
        {
            _container = new UnityContainer();
            RegisterTypes();
        }
        public static UnityClient ContainerObject
        {
            get 
            {
                return _unityClient ?? new UnityClient();
            }
        }
        public IUnityContainer GetContainer()
        {
            return _container;
        }
        private static void RegisterTypes()
        {
            _container.RegisterType<IFileProcessor<SecuritiesDto>, SecurityFileProcessor>();
            _container.RegisterType<IDirector<IFileProcessor<SecuritiesDto>>, XMLProcessor<SecuritiesDto>>();
            
            _container.RegisterType<IFileProcessor<List<TradeDto>>, TradeFileProcessor>();
            _container.RegisterType<IDirector<IFileProcessor<List<TradeDto>>>, XMLProcessor<List<TradeDto>>>();

            _container.RegisterType<IFileProcessor<List<TradesDto>>, TradeFileProcessorExtended>();
            _container.RegisterType<IDirector<IFileProcessor<List<TradesDto>>>, XMLProcessor<List<TradesDto>>>();
        }
    }
}
