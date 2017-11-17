using AuctionCrawler.ConsoleApp.Extensions;
using OpenQA.Selenium;
using System;
using System.Linq;

namespace AuctionCrawler.ConsoleApp.PageElements
{
    class CarLotPage
    {
        private readonly IWebDriver driver;

        internal CarLotPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public static CarLotPage Navigate(IWebDriver driver, string url)
        {
            driver.Url = url;
            driver.Navigate();
            driver.WaitToLoad();
            driver.WaitForElement(By.CssSelector("label[for=\"last-updated\"]"));
            driver.Sleep(500);

            return new CarLotPage(driver);
        }

        public string GetTitle()
        {
            string text = driver.FindElement(By.CssSelector(".lot-vehicle-info .title")).Text;
            text = text.Trim();
            return text;
        }

        public int? GetLotNumberN()
        {
            string text = driver.FindElement(By.CssSelector(".lot-number")).Text;
            text = text.ToLower().StripWhitespace();

            if (!text.StartsWith("lot#"))
            {
                return null;
            }

            return text.Substring(4).TakeWhile(c => Char.IsDigit(c)).ToNewString().ToInt();
        }

        public decimal? GetCurrentBidN()
        {
            string priceText = driver.FindElement(By.XPath("//label[@for=\"Current Bid\"]/following-sibling::span[1]")).Text;
            priceText = priceText.ToLower();
            if (!priceText.Contains("usd") && !priceText.Contains("$"))
            {
                return null;
            }

            priceText = priceText.StripNonNumeric();
            return priceText.ToDecimal();
        }

        public CarLotTimeLeft GetTimeLeft()
        {
            string text = driver.FindElement(By.XPath("//label[@for=\"time-left\"]/following-sibling::span[1]")).Text;
            text = text.ToLower().Trim();

            if (text.Contains("live"))
            {
                return new CarLotTimeLeft { IsLive = true };
            }
            else
            {
                TimeSpan? timeLeft = text.ParseTimeSpanDMS();
                if (timeLeft.HasValue)
                {
                    return new CarLotTimeLeft { TimeLeft = timeLeft.Value, IsSpecified = true };
                }
                else
                {
                    return new CarLotTimeLeft();
                }
            }
        }
    }
}
