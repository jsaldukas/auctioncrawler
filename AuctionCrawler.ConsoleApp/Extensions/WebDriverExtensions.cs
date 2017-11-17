using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;
using System.Threading;

namespace AuctionCrawler.ConsoleApp.Extensions
{
    static class WebDriverExtensions
    {
        public static void WaitToLoad(this IWebDriver driver, int timeoutSeconds = 30)
        {
            IWait<IWebDriver> wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutSeconds));
            wait.Until(driver1 => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
        }
        
        public static void WaitForElement(this IWebDriver driver, By by, int timeoutSeconds = 30)
        {
            IWait<IWebDriver> wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutSeconds));
            wait.Until(driver1 => driver1.FindElements(by).Any(x => x.Displayed));
        }

        public static void Sleep(this IWebDriver driver, int milliseconds)
        {
            Thread.Sleep(milliseconds);
        }
    }
}
