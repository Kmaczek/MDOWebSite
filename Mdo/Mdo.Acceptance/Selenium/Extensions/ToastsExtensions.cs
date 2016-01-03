using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace Mdo.Acceptance.Selenium.Extensions
{
    public static class ToastsExtensions
    {
        public static string GetToastType(this IWebElement message)
        {
            // ex.
            // toast-error

            return message.GetAttribute("class");
        }

        public static IWebElement GetToastTitle(this IWebElement message)
        {
            return message.FindElement(By.ClassName("toast-title"));
        }

        public static IWebElement GetToastMessage(this IWebElement message)
        {
            return message.FindElement(By.ClassName("toast-message"));
        }
    }
}
