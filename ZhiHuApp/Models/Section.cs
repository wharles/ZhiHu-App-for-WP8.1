using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZhiHuApp.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Section
    {
        [JsonProperty("timestamp")]
        public double Timestamp { get; set; }

        [JsonProperty("stories")]
        public IEnumerable<Story> Stories { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
