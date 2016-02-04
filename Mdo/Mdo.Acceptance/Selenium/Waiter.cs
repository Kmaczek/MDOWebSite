using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mdo.Acceptance.Selenium
{
    public class Waiter
    {
        public static T ForNotNull<T>(Func<T> func, TimeSpan span)
        {
            var timer = new Timer(state =>
            {
                while (func.Invoke() != null)
                {
                    Debug.WriteLine("invoked");
                }
            }, null, span, new TimeSpan(0,0,0,0,100));
            
            return func.Invoke();
        }

        public static T ForNotNull<T>(Func<T> func)
        {
            var span = TimeSpan.FromSeconds(3);
            var time = DateTime.Now;

            while (func.Invoke() == null && (DateTime.Now.Subtract(time)) < span)
            {
                Thread.Sleep(100);
            }

            return func.Invoke();
        }
    }
}
