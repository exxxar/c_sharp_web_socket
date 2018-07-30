using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static web_socket.CryptoDataElement_trades;

namespace web_socket
{
    class CryptoDataElement_trades
    {
        public class Trades
        {
            public enum TradeType
            {
                sell,
                buy
            }
            public long tid { get; set; }
            public TradeType type { get; set; }
            public long date { get; set; }
            public double price { get; set; }
            public double amount { get; set; }


        }

        public Trades[] trades { get; set; }
    }
}
