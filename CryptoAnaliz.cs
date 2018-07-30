using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace web_socket
{
    class CryptoAnaliz
    {
        private CryptoData head;
        private Dictionary<string, CryptoDataElement_tickers> tickers_dataList;
        private CryptoDataElement_at_mining at_mining_data;
        private Dictionary<string, CryptoDataElement_update> update_data = new Dictionary<string, CryptoDataElement_update>();
        private Dictionary<string, CryptoDataElement_trades> trades_data = new Dictionary<string, CryptoDataElement_trades>();
        public List<String> markets { get; set; } = new List<string>();

        public void update(String jsonString)
        {
            this.head = JsonConvert.DeserializeObject<CryptoData>(jsonString);
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
                            Console.WriteLine("[{0}]=>{1}", k.Key, k.Value);
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
