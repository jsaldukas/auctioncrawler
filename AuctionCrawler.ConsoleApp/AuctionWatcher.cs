using AuctionCrawler.ConsoleApp.Extensions;
using AuctionCrawler.ConsoleApp.PageElements;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionCrawler.ConsoleApp
{
    class AuctionWatcher
    {
        public AuctionWatcher()
        {
        }

        public async void Run()
        {
            using (var driver = new ChromeDriver())
            {
                CarAuctionPage carAuctionPage = CarAuctionPage.Navigate(driver, "https://www.copart.com/auctions/auctionDashboard");
                Console.WriteLine("Navigated to auction dashboard");
                carAuctionPage.JoinSearchLiveAuction(0);
                Console.WriteLine("Joined auction");
                while(!carAuctionPage.InjectMessageListenerHandler())
                {
                    driver.Sleep(1000);
                }
                Console.WriteLine("Injected JS");

                while(true)
                {
                    var messages = carAuctionPage.PopReceivedMessages();
                    Console.WriteLine($"Got {messages.Count} messages");
                    File.AppendAllLines("auction_messages.data", messages);
                    driver.Sleep(2000);
                }
            }
        }
    }
}
