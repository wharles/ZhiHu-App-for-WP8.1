using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZhiHuApp.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Themes
    {
        [JsonProperty("limit")]
        public int Limit { get; set; }

        [JsonProperty("subscribed")]
        public object[] Subscribed { get; set; }

        [JsonProperty("others")]
        public IEnumerable<Others> Others { get; set; }
    }
    [JsonObject(MemberSerialization.OptIn)]
    public class Others
    {

        [JsonProperty("color")]
        public int Color { get; set; }

        [JsonProperty("thumbnail")]
        public string Thumbnail { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
