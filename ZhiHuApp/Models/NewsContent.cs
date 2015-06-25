using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZhiHuApp.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class NewsContent
    {
        [JsonProperty("body")]
        public string Body { get; set; }

        [JsonProperty("image_source")]
        public string ImageSource { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }

        [JsonProperty("share_url")]
        public string ShareUrl { get; set; }

        [JsonProperty("js")]
        public object[] Js { get; set; }

        [JsonProperty("ga_prefix")]
        public string GaPrefix { get; set; }

        [JsonProperty("type")]
        public int Type { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("css")]
        public string[] Css { get; set; }
    }
}
