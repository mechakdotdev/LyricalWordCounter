using Newtonsoft.Json;

public class LyricsResponse
{
    [JsonProperty(PropertyName = "lyrics")]
    public string? Lyrics { get; set; }
}