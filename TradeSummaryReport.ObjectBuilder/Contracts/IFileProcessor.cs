using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeSummaryReport.FileDataBuilder.Contracts
{
    public interface IFileProcessor<T>
    {
        string Path { get; set; }
        void ReadFile();
        void ProcessObject();
        T OutputObject { get; }
    }
}
