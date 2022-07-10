using Newtonsoft.Json;

public class DeezerData
{
    [JsonProperty(PropertyName = "id")]
    public int Id { get; set; }

    [JsonProperty(PropertyName = "title")]
    public string? Title { get; set; }

    [JsonProperty(PropertyName = "link")]
    public string? Link { get; set; }
}