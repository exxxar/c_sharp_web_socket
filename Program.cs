using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;

namespace web_socket
{
    class Program
    {

        static void Main(string[] args)
        {
            int steps = 0;
            CryptoAnaliz ca = new CryptoAnaliz();

            try
            {
                using (var ws = new WebSocket("wss://push.abcc.com/app/2d1974bfdde17e8ecd3e7f0f6e39816b?protocol=7&client=js&version=4.2.2&flash=falsea"))
                {
                    ws.OnMessage += (sender, e) =>
                    {

                        if (steps == 0) //авторизириуемся на сокете
                        {
                       
                            JObject request = new JObject(
                                  new JProperty("event", "pusher:subscribe"),
                                  new JProperty("data", new JObject(
                                      new JProperty(
                                          "channel", "market-global"
                                          )                                     
                                      )
                                  )
                             );
                            ws.Send(JsonConvert.SerializeObject(request));
                        }
                                              

                        if (steps >= 2)                        
                            ca.update(e.Data);

                        if (steps == 2)
                        {
                            ca.markets.ForEach(m =>
                            {
                                JObject request = new JObject(
                                  new JProperty("event", "pusher:subscribe"),
                                  new JProperty("data", new JObject(
                                      new JProperty(
                                          "channel", $"market-{m}-global"
                                          )
                                      )
                                  )
                             );
                                ws.Send(JsonConvert.SerializeObject(request));
                            });

                        }

                        steps++;
                    };
                    ws.Connect();
                    Console.ReadKey(true);
                }
            }
            catch { }
        }
    }
}
