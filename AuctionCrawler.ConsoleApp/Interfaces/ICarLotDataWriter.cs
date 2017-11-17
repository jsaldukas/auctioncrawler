using AuctionCrawler.ConsoleApp.Data;
using System;

namespace AuctionCrawler.ConsoleApp.Interfaces
{
    interface ICarLotDataWriter : IDisposable
    {
        void WriteRow(CarLotDataRow row);
    }
}
