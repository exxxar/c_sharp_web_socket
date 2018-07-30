using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace web_socket
{
    public class CryptoData
    {

        [JsonProperty("event")]
        public string _event { get; set; }
        public string data { get; set; }
        public string channel { get; set; }

        public override string ToString()
        {
            return String.Format("{0} {1} {2}", _event, data, channel);
        }

    }
}
