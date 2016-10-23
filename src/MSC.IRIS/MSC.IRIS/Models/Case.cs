using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MSC.IRIS.Models
{
    /// <summary>
    /// This is a case that has been identified by the backend system to be monitored on
    /// </summary>
    public class Case
    {
        public string Id { get; set; }
        [JsonProperty("registered_account_id")]
        public string RegisteredAccountId { get; set; }
        [JsonProperty ("is_archived")]
        public bool IsArchived { get; set; }
        [JsonProperty ("archived_by")]
        public int ArchivedBy { get; set; }
        [JsonProperty ("last_update")]
        public string LastUpdate { get; set; }
        [JsonProperty ("created_date")]
        public string CreatedDate { get; set; }
        [JsonProperty ("log")]
        public double? Longitude { get; set; }
        [JsonProperty ("lat")]
        public double? Latitude { get; set; }
        public string ProfilePic { get; set; }
        public List<string> photos { get; set; }
        [JsonProperty ("social_content")]
        public List<SocialContent> SocialContent { get; set; }

        public string AccountName
        {
            get
            {
                return $"TODO ({RegisteredAccountId})";
            }
        }
        public string Location
        {
            get
            {
                return $"{(Latitude.HasValue ? Latitude.Value : 0)}, {(Longitude.HasValue ? Longitude.Value : 0)}";
            }
        }
    }
}
