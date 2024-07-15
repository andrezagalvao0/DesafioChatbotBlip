using System.Text.Json.Serialization;

namespace Api.Models
{
    public class GithubRepository
    {
        [JsonPropertyName("full_name")]
        public string FullName { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("avatar_url")]
        public string AvatarUrl { get; set; }
        [JsonIgnore]
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }
    }
}
