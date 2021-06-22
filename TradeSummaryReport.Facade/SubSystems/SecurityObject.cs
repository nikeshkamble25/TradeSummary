using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeSummaryReport.Dto;
using TradeSummaryReport.FileDataBuilder.Contracts;
using TradeSummaryReport.ObjectBuilder.Builder.Contracts;
using Unity;

namespace TradeSummaryReport.Facade.SubSystems
{
    internal class SecurityObject
    {
        internal SecuritiesDto GetSecurities(string path)
        {
            IFileProcessor<SecuritiesDto> fileProcessor = UnityClient.ContainerObject.GetContainer()
                .Resolve<IFileProcessor<SecuritiesDto>>();
            fileProcessor.Path = path;
            IDirector<IFileProcessor<SecuritiesDto>> objectProcessor = UnityClient.ContainerObject.GetContainer()
                .Resolve<IDirector<IFileProcessor<SecuritiesDto>>>();
            objectProcessor.ConstructObject(fileProcessor);
            return fileProcessor.OutputObject;
        }
    }
}
