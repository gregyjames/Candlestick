using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candlestick.Models
{
    public class StockPrice
    {
        public DateTime Date { get; set; }
        public Decimal Open { get; set; }
        public Decimal High { get; set; }
        public Decimal Low { get; set; }
        public Decimal Close { get; set; }
    }
}
