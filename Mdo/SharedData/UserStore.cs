using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedData
{
    public class UserStore: MarshalByRefObject
    {
        private static int counter = 0;
        public int Add(int a, int b)
        {
            return a + b;
        }

        public int Count()
        {
            counter++;
            return counter;
        }
    }
}
