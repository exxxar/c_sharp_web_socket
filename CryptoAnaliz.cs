using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace web_socket
{
   public class CryptoAnaliz
    {
        public enum CryptoSortDirection
        {
            ASK,DESK
        }
        public enum CryptoSortType
        {
            ID,NAME,BASEUNIT, QUOTEUNIT,ASKFIXED,BIDFIXED,LOW,HIGH,LAST,BUY,SELL,OPEN,CHANGE,VOLUME,FUNDS,AT
        }

        public CryptoData head;
        public Dictionary<string, CryptoDataElement_tickers> tickers_dataList;
        private CryptoDataElement_at_mining at_mining_data;
        private Dictionary<string, CryptoDataElement_update> update_data = new Dictionary<string, CryptoDataElement_update>();
        private Dictionary<string, CryptoDataElement_trades> trades_data = new Dictionary<string, CryptoDataElement_trades>();
        public List<String> markets { get; set; } = new List<string>();

        public void sort(CryptoSortType sort=CryptoSortType.NAME)
        {

            IOrderedEnumerable<KeyValuePair<string, CryptoDataElement_tickers>> items = null;

            switch (sort)
            {
                case CryptoSortType.NAME:   items = from pair in tickers_dataList orderby pair.Value.name ascending select pair; break;
                case CryptoSortType.ID:   items = from pair in tickers_dataList orderby pair.Value.id ascending select pair; break;
                case CryptoSortType.BASEUNIT: items = from pair in tickers_dataList orderby pair.Value.base_unit ascending select pair; break;
                case CryptoSortType.QUOTEUNIT: items = from pair in tickers_dataList orderby pair.Value.quote_unit ascending select pair; break;
                case CryptoSortType.ASKFIXED: items = from pair in tickers_dataList orderby pair.Value.ask_fixed ascending select pair; break;
                case CryptoSortType.BIDFIXED: items = from pair in tickers_dataList orderby pair.Value.bid_fixed ascending select pair; break;
                case CryptoSortType.LOW: items = from pair in tickers_dataList orderby pair.Value.low ascending select pair; break;
                case CryptoSortType.HIGH: items = from pair in tickers_dataList orderby pair.Value.high ascending select pair; break;
                case CryptoSortType.LAST: items = from pair in tickers_dataList orderby pair.Value.last ascending select pair; break;
                case CryptoSortType.BUY: items = from pair in tickers_dataList orderby pair.Value.buy ascending select pair; break;
                case CryptoSortType.SELL: items = from pair in tickers_dataList orderby pair.Value.sell ascending select pair; break;
                case CryptoSortType.OPEN: items = from pair in tickers_dataList orderby pair.Value.open ascending select pair; break;
                case CryptoSortType.CHANGE: items = from pair in tickers_dataList orderby pair.Value.change ascending select pair; break;
                case CryptoSortType.VOLUME: items = from pair in tickers_dataList orderby pair.Value.volume ascending select pair; break;
                case CryptoSortType.FUNDS: items = from pair in tickers_dataList orderby pair.Value.funds ascending select pair; break;
                case CryptoSortType.AT: items = from pair in tickers_dataList orderby pair.Value.at ascending select pair; break;
            }
            // Display results.
            foreach (KeyValuePair<string, CryptoDataElement_tickers> pair in items)
            {
                Console.WriteLine("{0}: {1}", pair.Key, pair.Value);
            }

        }
        public void update(String jsonString)
        {
            this.head = JsonConvert.DeserializeObject<CryptoData>(jsonString);
            Console.WriteLine(this.head._event);
            switch (head._event.ToLower().Trim())
            {
                case "tickers":
                    
                    this.tickers_dataList = JsonConvert.DeserializeObject<Dictionary<string, CryptoDataElement_tickers>>(this.head.data);

                    this.markets.Clear();
                    this.tickers_dataList
                        .ToList()
                        .ForEach(k =>
                        {
                            markets.Add(k.Key);
                            
                        });

                    
                    break;
                case "at-mining":

                    this.at_mining_data = JsonConvert.DeserializeObject<CryptoDataElement_at_mining>(this.head.data);
                    Console.WriteLine(at_mining_data);
                    break;

                case "trades": 
                    bool inDict_trades = false;
                    CryptoDataElement_trades cdeT = JsonConvert.DeserializeObject<CryptoDataElement_trades>(this.head.data);
                    if (trades_data.Count > 0)
                        trades_data.ToList().ForEach(ud =>
                        {
                            if (ud.Key.Equals(head.channel))
                            {
                                inDict_trades = true;
                                return;
                            }
                        });

                    if (!inDict_trades)
                        trades_data.Add(head.channel, cdeT);
                    else
                        trades_data[head.channel] = cdeT;

                    trades_data.ToList().ForEach(td =>
                    {
                        Console.WriteLine("***********************************\n\t{0}}\n***********************************\n\t", td.Value.trades[0].amount);
                    });
                    break;
                case "update":
                  
                    bool inDict_updates = false;                    
                    CryptoDataElement_update cdeU = JsonConvert.DeserializeObject<CryptoDataElement_update>(this.head.data);
                    if (update_data.Count > 0)
                        update_data.ToList().ForEach(ud =>
                        {
                            if (ud.Key.Equals(head.channel))
                            {
                                inDict_updates = true;
                                return;
                            }
                        });

                    if (!inDict_updates)
                        update_data.Add(head.channel, cdeU);
                    else
                       update_data[head.channel] = cdeU;                  
                    break;

            }



        }
    }

}
