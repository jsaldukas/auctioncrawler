using AuctionCrawler.ConsoleApp.Extensions;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionCrawler.ConsoleApp.PageElements
{
    class CarAuctionPage
    {
        private readonly IWebDriver driver;

        internal CarAuctionPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public static CarAuctionPage Navigate(IWebDriver driver, string url)
        {
            driver.Url = url;
            driver.Navigate();
            driver.WaitToLoad();
            driver.WaitForElement(By.CssSelector("iframe#iAuction5"));
            driver.SwitchTo().Frame("iAuction5");
            driver.WaitForElement(By.CssSelector(".arAuctiontable .searchDiv"));
            driver.Sleep(1000);

            return new CarAuctionPage(driver);
        }

        public bool Search(string text)
        {
            driver.FindElement(By.CssSelector("input.searchInputTxt")).SendKeys(text);
            driver.Sleep(500);
            driver.FindElement(By.CssSelector("button.searchBtn")).Click();
            return driver.WaitForElement(By.CssSelector("div.noResultsAlert:visible"), 3);
        }

        public int GetSearchLiveAuctionCount()
        {
            return driver.FindElements(By.CssSelector("tr.liveAuction")).Count();
        }

        public void JoinSearchLiveAuction(int index)
        {
            var auctionsJoinBtns = driver.FindElements(By.CssSelector($"tr.liveAuction[id]"));
            auctionsJoinBtns[index].Click();
        }

        public bool IsMessageListenerInjected()
        {
            return driver.ExecuteJsFunction<bool>("return !!window.___messagesBucketThingEnabled");
        }

        public bool InjectMessageListenerHandler()
        {
            //CopartTVLayoutManager.messageHandler.onMessage = function (m) { console.log(JSON.stringify(m)); return onmsg(m); };
            if(!driver.ExecuteJsFunction<bool>("return !!CopartTVLayoutManager.messageHandler"))
            {
                return false;
            }

            driver.ExecuteJs(@"
                window.___originalMessagesHandler = CopartTVLayoutManager.messageHandler.onMessage;
                window.___messagesBucket = [];
                window.___messagesBucketFiller = function(a,b,c,d,e,f) {
                    window.___messagesBucket.push(JSON.stringify(a));
                    return window.___originalMessagesHandler(a,b,c,d,e,f);
                }
                window.___messagesBucketEmptier = function() {
                    var msgs = window.___messagesBucket;
                    window.___messagesBucket = [];
                    return msgs;
                }
                CopartTVLayoutManager.messageHandler.onMessage = window.___messagesBucketFiller;
                window.___messagesBucketThingEnabled = true;
            ");

            return IsMessageListenerInjected();
        }

        public IList<string> PopReceivedMessages()
        {
            return driver.ExecuteJsFunction<IReadOnlyCollection<object>>("return window.___messagesBucketEmptier();").Select(x => x.ToString()).ToList();
        }
    }
}
