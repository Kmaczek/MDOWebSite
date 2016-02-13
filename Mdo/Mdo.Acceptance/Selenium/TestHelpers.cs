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
        private static readonly Uri baseUri = new Uri(@"http://localhost:22222/");
        private static readonly string userRegisterPath = "user/register";
        private static readonly string userProfilePath = "profile";
        private static readonly string messagesPath = "message";

        public static string PageUrl => baseUri.ToString();

        public static string UserProfileUrl => new Uri(baseUri, userProfilePath).ToString();

        public static string UserRegisterUrl => new Uri(baseUri, userRegisterPath).ToString();

        public static string MessagesUrl => new Uri(baseUri, messagesPath).ToString();

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
