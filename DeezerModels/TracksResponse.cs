using Newtonsoft.Json;

public class TracksResponse
{
    [JsonProperty(PropertyName = "data")]
    public List<DeezerData>? Data { get; set; }
}