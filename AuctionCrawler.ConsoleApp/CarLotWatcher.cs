using AuctionCrawler.ConsoleApp.Data;
using AuctionCrawler.ConsoleApp.Interfaces;
using AuctionCrawler.ConsoleApp.PageElements;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AuctionCrawler.ConsoleApp
{
    class CarLotWatcher
    {
        private readonly string[] urls;
        private readonly int intervalSeconds;
        private readonly ICarLotDataWriter carLotDataWriter;
        private readonly CancellationToken cancellationToken;

        public CarLotWatcher(string[] urls, int intervalSeconds, ICarLotDataWriter carLotDataWriter, CancellationToken cancellationToken)
        {
            this.urls = urls;
            this.intervalSeconds = intervalSeconds;
            this.carLotDataWriter = carLotDataWriter;
            this.cancellationToken = cancellationToken;
        }

        public async void Run()
        {
            using (var driver = new ChromeDriver())
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    foreach (var url in urls)
                    {
                        var carLotPage = CarLotPage.Navigate(driver, url);
                        var lotNumber = carLotPage.GetLotNumberN().GetValueOrDefault();
                        if (lotNumber == 0)
                        {
                            continue;
                        }

                        var timeLeft = carLotPage.GetTimeLeft();

                        var row = new CarLotDataRow
                        {
                            Timestamp = DateTime.Now,
                            LotNumber = lotNumber,
                            CurrentBid = carLotPage.GetCurrentBidN(),
                            TimeLeft = timeLeft.IsSpecified ? timeLeft.TimeLeft : (TimeSpan?)null,
                            Title = carLotPage.GetTitle()
                        };
                        
                        carLotDataWriter.WriteRow(row);

                        Console.WriteLine($"{row.Timestamp:yyyy-MM-dd HH:mm:ss}: Row written for {url}");
                    }

                    try
                    {
                        await Task.Delay(intervalSeconds * 1000, cancellationToken);
                    } catch(TaskCanceledException)
                    {
                        break;
                    }
                }

                driver.Quit();
            }
        }
    }
}
