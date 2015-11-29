using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mdo.Website.Common
{
    public interface IMdoLogger
    {
        void Write(string message);
        void Warning(string message);
        void Info(string message);
    }
}
