using AuctionCrawler.ConsoleApp.DataWriter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AuctionCrawler.ConsoleApp
{
    class ProgramParameters
    {
        public IEnumerable<string> Urls { get; set; }
        public int IntervalSeconds { get; set; } = 60;
    }

    class Program
    {
        static void Main(string[] args)
        {
            var auctionWatcher = new AuctionWatcher();
            auctionWatcher.Run();
            //var parameters = Args.Configuration.Configure<ProgramParameters>().CreateAndBind(args);
            //CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

            //using (var carLotDataWriter = new CsvCarLotDataWriter("carlotdata.csv"))
            //{
            //    var carLotWatcher = new CarLotWatcher(parameters.Urls.ToArray(), parameters.IntervalSeconds, carLotDataWriter, cancellationTokenSource.Token);

            //    var task = Task.Run(() => carLotWatcher.Run());

            //    Console.WriteLine("Press any key to exit...");
            //    Console.ReadLine();
            //    cancellationTokenSource.Cancel();

            //    Console.WriteLine("Exiting...");
            //    task.Wait(30000);
            //}
        }
    }
}
