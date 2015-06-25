using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ZhiHuApp.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class SectionStory
    {

        [JsonProperty("images")]
        public string[] Images { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("display_date")]
        public string DisplayDate { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class SectionDetial
    {

        [JsonProperty("timestamp")]
        public int Timestamp { get; set; }

        [JsonProperty("stories")]
        public IEnumerable<SectionStory> Stories { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

}

