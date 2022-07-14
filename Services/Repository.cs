using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LyricsAPIClient.Repositories
{
    public class Repository
    {
        public static async Task<LyricsResponse> GetLyricsAsync(LyricsRequest lyricsRequest)
        {
            Console.WriteLine("Getting lyrics...\nPlease wait...\n");

            var url = ($"https://api.lyrics.ovh/v1/{lyricsRequest.Artist}/{lyricsRequest.Title}");

            HttpClient client = new();
            using HttpResponseMessage response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                using HttpContent content = response.Content;
                var jsonResponse = await content.ReadAsStringAsync();
                var contentAsJToken = JsonConvert.DeserializeObject<JToken>(jsonResponse)
                     ?? throw new Exception("Lyric.ovh API response failed to deserialize as JToken.");
                var lyricsResponse = contentAsJToken.ToObject<LyricsResponse>()
                    ?? throw new Exception("Lyric.ovh response could not be mapped to object LyricsResponse.");
                
                Console.WriteLine("SUCCESS: Lyrics found.\n");

                return lyricsResponse;
            }
            else
            {
                Console.WriteLine($"UNSUCCESSFUL: Lyrics not found. Returning empty set of lyrics. This song will not be included when calculating the average.");
                return new LyricsResponse { Lyrics = null };
            }
        }

        public static async Task<TracksResponse> GetAllTracksAsync(string chosenArtist)
        {
            Console.WriteLine($"Getting all tracks by {chosenArtist}...\nPlease wait...\n");

            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://api.deezer.com/search?q={chosenArtist}")
            };
            using (var response = await client.SendAsync(request))
            {
                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();

                    var contentAsJToken = JsonConvert.DeserializeObject<JToken>(jsonResponse)
                         ?? throw new Exception("Deezer API response failed to deserialize as JToken.");

                    var deezerData = contentAsJToken.ToObject<TracksResponse>()
                        ?? throw new Exception("Deezer API data could not be mapped to object DeezerData.");

                    return deezerData;
                }
                else
                {
                    throw new Exception($"No tracks were found for this artist. Please check the name you entered try again.");
                }
            }
        }

        public static int GetWordCount(string lyrics)
        {
            int wordCount = 0, index = 0;

            // skip whitespace until first word is found
            while (index < lyrics.Length && char.IsWhiteSpace(lyrics[index]))
                index++;

            while (index < lyrics.Length)
            {
                while (index < lyrics.Length && !char.IsWhiteSpace(lyrics[index]))
                    index++;
                    wordCount++;

                while (index < lyrics.Length && char.IsWhiteSpace(lyrics[index]))
                    index++;
            }

            return wordCount;
        }
    }
}