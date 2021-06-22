using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeSummaryReport.ObjectBuilder.Builder.Contracts
{
    public interface IDirector<T>
    {
        void ConstructObject(T builder);
    }
}
