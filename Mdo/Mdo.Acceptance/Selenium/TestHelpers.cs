using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mdo.Acceptance.Selenium
{
    static class TestHelpers
    {
        public static void WaitSomeTime(int timeInMs = 100)
        {
            Thread.Sleep(new TimeSpan(0, 0, 0, 0, timeInMs));
        }

        public static void WaitForHttpRequest(int timeInMs = 200)
        {
            Thread.Sleep(new TimeSpan(0, 0, 0, 0, timeInMs));
        }
    }
}
