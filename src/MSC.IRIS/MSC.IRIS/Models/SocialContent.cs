using System;
using Newtonsoft.Json;

namespace MSC.IRIS.Models
{
    public class SocialContent
    {
        public string Id { get; set; }
        [JsonProperty("social_site")]
        public string SocialSite { get; set; }
        public string Content { get; set; }
        public bool IsTwitter
        {
            get
            {
                return SocialSite.ToLower () == "twitter";
            }
        }

        public string SocialContentImage
        {
            get
            {
                var ss = SocialSite.ToLower ();
                if (ss == "twitter")
                    return "twitter.png";
                else
                    return "facebook.png";
            }
        }
    }
}
