using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZhiHuApp.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class StartImage
    {
        [JsonProperty("text")]
        public string Text { set; get; }
        [JsonProperty("img")]
        public string Img { set; get; }
    }
}
