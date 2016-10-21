using JPMorgan.Assignment.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPMorgan.Assignment
{
   public  class Trade
    {
        public TradeType Type { get; set; }       
        public int Quantity { get; set; }
        public Double Price { get; set; }
        public DateTime TradeTime { get; set; }
    }
}
