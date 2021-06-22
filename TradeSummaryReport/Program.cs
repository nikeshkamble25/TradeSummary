using System;
using TradeSummaryReport.Facade;

namespace TradeSummaryReport
{
    class Program
    {
        static int tableWidth = 100;
        static void Main(string[] args)
        {
            string tradePath = @"C:\Users\nikes\OneDrive\Desktop\Engineer Code Test\Backend\Test";
            string securityPath = @"C:\Users\nikes\OneDrive\Desktop\Engineer Code Test\Backend\Securities.xml";
            PrintLine();
            Console.WriteLine("Please Select Options Below");
            PrintLine();
            PrintRow("1 - Load Trade Summary", "2 - Load Impure Trade File");
            PrintLine();
            Console.Write("Enter Option: ");
            string input = Console.ReadLine();
            if (input == "1")
                DisplaySummaryReport(tradePath, securityPath);
            else if (input == "2")
                DisplayImpureReport(tradePath, securityPath);
            else
                Console.WriteLine("InValid input*********");
            Console.Read();
        }
        static void DisplaySummaryReport(string tradePath, string securityPath)
        {
            ObjectReader objectReader = new ObjectReader();
            var tradeAggreList = objectReader.GetAggregatedTrades(tradePath, securityPath);
            Console.Clear();
            PrintLine();
            PrintRow("BloombergId", "TransCode", "TradeDate", "Sum(Qnt)", "Avg(Price)");
            PrintLine();
            foreach (var trade in tradeAggreList)
            {
                PrintRow(trade.BloombergId,
                    trade.TransactionCode,
                    trade.TradeDate.ToString(),
                    trade.QuantitySum.ToString(),
                    trade.PriceAvg.ToString());
            }
            PrintLine();
        }
        static void DisplayImpureReport(string tradePath, string securityPath)
        {
            ObjectReader objectReader = new ObjectReader();
            var impureList = objectReader.GetImpureTrades(tradePath, securityPath);
            Console.Clear();
            PrintLine();
            PrintRow("Below are impure files");
            PrintLine();
            foreach (var trade in impureList)
            {
                PrintLine();
                Console.WriteLine(trade.Path);
                PrintRow($"Number of impure trade in {trade.Trade.Count}");
                PrintLine();
            }
            PrintLine();
        }
        static void PrintLine()
        {
            Console.WriteLine(new string('-', tableWidth));
        }
        static void PrintRow(params string[] columns)
        {
            int width = (tableWidth - columns.Length) / columns.Length;
            string row = "|";
            foreach (string column in columns)
            {
                row += AlignCentre(column, width) + "|";
            }
            Console.WriteLine(row);
        }
        static string AlignCentre(string text, int width)
        {
            text = text.Length > width ? text.Substring(0, width - 3) + "..." : text;
            if (string.IsNullOrEmpty(text))
            {
                return new string(' ', width);
            }
            else
            {
                return text.PadRight(width - (width - text.Length) / 2).PadLeft(width);
            }
        }
    }
}
