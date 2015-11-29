using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mdo.Website.Common
{
    public class ConsoleLogger : IMdoLogger
    {
        public void Write(string message)
        {
            Console.WriteLine(message);
        }

        public void Warning(string message)
        {
            throw new NotImplementedException();
        }

        public void Info(string message)
        {
            throw new NotImplementedException();
        }
    }
}
