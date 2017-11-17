using AuctionCrawler.ConsoleApp.Data;
using AuctionCrawler.ConsoleApp.Interfaces;
using CsvHelper;
using System.IO;

namespace AuctionCrawler.ConsoleApp.DataWriter
{
    class CsvCarLotDataWriter : ICarLotDataWriter
    {
        StreamWriter appendWriter;
        CsvWriter csvWriter;

        public CsvCarLotDataWriter(string filePath)
        {
            appendWriter = File.AppendText(filePath);
            csvWriter = new CsvWriter(appendWriter);
            if (appendWriter.BaseStream.Position == 0)
            {
                csvWriter.WriteHeader<CarLotDataRow>();
                csvWriter.NextRecord();
            }
        }

        public void Dispose()
        {
            csvWriter.Dispose();
            appendWriter.Close();
        }

        public void WriteRow(CarLotDataRow row)
        {
            csvWriter.WriteRecord(row);
            csvWriter.NextRecord();
        }
    }
}
