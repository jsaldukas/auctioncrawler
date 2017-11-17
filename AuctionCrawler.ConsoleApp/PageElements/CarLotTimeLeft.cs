using System;

namespace AuctionCrawler.ConsoleApp.PageElements
{
    class CarLotTimeLeft
    {
        public bool IsLive { get; set; }
        public bool IsSpecified { get; set; }
        public TimeSpan TimeLeft { get; set; }
    }
}
