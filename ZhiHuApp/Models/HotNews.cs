using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZhiHuApp.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class HotNews
    {
        [JsonProperty("recent")]
        public IEnumerable<Recent> Recent { get; set; }
    }
    [JsonObject(MemberSerialization.OptIn)]
    public class Recent
    {
        [JsonProperty("news_id")]
        public int Id { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("thumbnail")]
        public string Thumbnail { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }
    }

}
