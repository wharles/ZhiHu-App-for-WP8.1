using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZhiHuApp.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class LatestNews
    {
        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("stories")]
        public IEnumerable<Story> Stories { get; set; }

        [JsonProperty("top_stories")]
        public IEnumerable<TopStory> TopStories { get; set; }
    }
    [JsonObject(MemberSerialization.OptIn)]
    public class TopStory
    {

        [JsonProperty("image")]
        public string Image { get; set; }

        [JsonProperty("type")]
        public int Type { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("ga_prefix")]
        public string GaPrefix { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }
    }
    [JsonObject(MemberSerialization.OptIn)]
    public class Story
    {

        [JsonProperty("images")]
        public string[] Images { get; set; }

        [JsonProperty("type")]
        public int Type { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("ga_prefix")]
        public string GaPrefix { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("multipic")]
        public bool? Multipic { get; set; }
    }
}
