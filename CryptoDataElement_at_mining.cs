using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace web_socket
{
    public class CryptoDataElement_at_mining
    {
        public class CurrentProfit
        {
            public Dictionary<string, string> beu { get; set; }
            public string current_date { get; set; }

            public override string ToString()
            {
                StringBuilder stb = new StringBuilder();
                beu.ToList().ForEach(el =>
                {
                    stb.AppendFormat(" {0}:{1} ", el.Key, el.Value);
                });
                return String.Format("[{0}] {1}", stb.ToString(), current_date);
            }
        }

        public class ComputingPower
        {
            public class Blocks
            {
                public Dictionary<string, string> beu { get; set; }
                public string key { get; set; }
                public string start_date { get; set; }
                public string end_date { get; set; }

                public override string ToString()
                {
                    StringBuilder stb = new StringBuilder();
                    beu.ToList().ForEach(el =>
                    {
                        stb.AppendFormat(" {0}:{1} ", el.Key, el.Value);
                    });
                    return String.Format("[{0}] {1} {2} {3}", stb.ToString(), key, start_date, end_date);
                }

            }
            public int count { get; set; }
            public int estimated { get; set; }
            public List<Blocks> blocks { get; set; }

            public override string ToString()
            {
                StringBuilder stb = new StringBuilder();
                blocks.ToList().ForEach(b => stb.AppendFormat(" {0} ", b));
                return String.Format("{0} {1} \n {2}", count, estimated, stb.ToString());
            }

        }
        public CurrentProfit current_profit { get; set; }
        public ComputingPower computing_power { get; set; }

        public override string ToString()
        {
            return String.Format("{0} \n{1}", current_profit, computing_power);
        }

    }
}
