using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZhiHuApp.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class StoryExtra
    {
        [JsonProperty("long_comments")]
        public int LongComments { get; set; }

        [JsonProperty("popularity")]
        public int Popularity { get; set; }

        [JsonProperty("short_comments")]
        public int ShortComments { get; set; }

        [JsonProperty("comments")]
        public int Comments { get; set; }
    }
}
