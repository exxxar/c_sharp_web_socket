using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace web_socket
{
    public class CryptoDataElement_tickers
    {
        public string id { get; set; }
        public string name { get; set; }
        public string base_unit { get; set; }
        public string quote_unit { get; set; }
        public string ask_fixed { get; set; }
        public string bid_fixed { get; set; }
        public string low { get; set; }
        public string high { get; set; }
        public string last { get; set; }
        public string buy { get; set; }
        public string sell { get; set; }
        public string open { get; set; }
        public string change { get; set; }
        public string volume { get; set; }
        public string funds { get; set; }
        public string at { get; set; }

        public override string ToString()
        {
            return String.Format($"id = {id} [ \n\t name={name} \n\t base_unit={base_unit} \n\t quote_unit={quote_unit} \n\t ask_fixed={ask_fixed} \n\t bid_fixed={bid_fixed} \n\t low={low} \n\t high={high} \n\t last={last} \n\t buy={buy} \n\t sell={sell} \n\t open={open} \n\t change={change} \n\t volume={volume} \n\t funds={funds} \n\t At={at} \n]");
        }
    }
}
