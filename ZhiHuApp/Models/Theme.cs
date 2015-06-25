using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using GalaSoft.MvvmLight;

namespace ZhiHuApp.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Editor
    {

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("bio")]
        public string Bio { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("avatar")]
        public string Avatar { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class Theme : ObservableObject
    {
        private IEnumerable<Story> stories;
        [JsonProperty("stories")]
        public IEnumerable<Story> Stories
        {
            get
            {
                return stories;
            }
            set
            {
                stories = value;
                RaisePropertyChanged(() => Stories);
            }
        }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("background")]
        public string Background { get; set; }

        [JsonProperty("color")]
        public int Color { get; set; }

        private string name;
        [JsonProperty("name")]
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                RaisePropertyChanged(() => Name);
            }
        }

        [JsonProperty("image")]
        public string Image { get; set; }

        private IEnumerable<Editor> editors;
        [JsonProperty("editors")]
        public IEnumerable<Editor> Editors
        {
            get
            {
                return editors;
            }
            set
            {
                editors = value;
                RaisePropertyChanged(() => Editors);
            }
        }

        [JsonProperty("image_source")]
        public string ImageSource { get; set; }
    }

}

