using System;

namespace AuctionCrawler.ConsoleApp.Data
{
    class CarLotDataRow
    {
        public DateTime Timestamp { get; set; }
        public int LotNumber { get; set; }
        public string Title { get; set; }
        public decimal? CurrentBid { get; set; }
        public TimeSpan? TimeLeft { get; set; }
    }
}
