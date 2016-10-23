using System;
using Newtonsoft.Json;

namespace MSC.IRIS.Models
{
    public class SocialContent
    {
        public int Id { get; set; }
        [JsonProperty("social_site")]
        public string SocialSite { get; set; }
        public string Content { get; set; }
    }
}
