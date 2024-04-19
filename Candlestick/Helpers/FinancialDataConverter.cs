using System;
using System.Collections.Generic;
using System.Linq;
using Candlestick.Models;

namespace Candlestick.Helpers
{
    public class FinancialDataConverter
    {
        public static List<StockPrice> ConvertInterval(List<StockPrice> data, string interval)
        {
            // Group by interval using Timestamp
            var groupedData = data
                .GroupBy(x => RoundDownToInterval(x.Date, interval))
                .Select(group => new StockPrice
                {
                    Date = group.Key,
                    Open = group.First().Open,
                    High = group.Max(g => g.High),
                    Low = group.Min(g => g.Low),
                    Close = group.Last().Close
                })
                .OrderBy(x => x.Date)
                .ToList();

            return groupedData;
        }

        private static DateTime RoundDownToInterval(DateTime date, string interval)
        {
            switch (interval.ToLower())
            {
                case "daily":
                    return date.Date;
                case "weekly":
                    return date.AddDays(-(int)date.DayOfWeek);
                case "monthly":
                    return new DateTime(date.Year, date.Month, 1);
                case "yearly":
                    return new DateTime(date.Year, 1, 1);
                default:
                    // Handle hours and minutes directly
                    return HandleCustomTimeIntervals(date, interval);
            }
        }

        private static DateTime HandleCustomTimeIntervals(DateTime date, string interval)
        {
            if (interval.EndsWith("h"))
            {
                int hours = int.Parse(interval.TrimEnd('h'));
                return new DateTime(date.Year, date.Month, date.Day, date.Hour / hours * hours, 0, 0);
            }
            else if (interval.EndsWith("m"))
            {
                int minutes = int.Parse(interval.TrimEnd('m'));
                return new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute / minutes * minutes, 0);
            }
            else
            {
                throw new ArgumentException("Unsupported interval type. Please use 'daily', 'weekly', 'monthly', 'yearly', 'XXh', or 'XXm'.");
            }
        }
    }
}
