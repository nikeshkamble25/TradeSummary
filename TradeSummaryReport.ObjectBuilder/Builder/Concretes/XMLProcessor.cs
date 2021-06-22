using TradeSummaryReport.FileDataBuilder.Contracts;
using TradeSummaryReport.ObjectBuilder.Builder.Contracts;

namespace TradeSummaryReport.ObjectBuilder.Builder.Concretes
{
    public class XMLProcessor<T> : IDirector<IFileProcessor<T>>
    {
        public void ConstructObject(IFileProcessor<T> builder)
        {
            builder.ReadFile();
            builder.ProcessObject();
        }
    }
}
