using LyricsAPIClient.Repositories;

namespace LyricsAPIClient
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();

        public static async Task Main(string[] args)
        {
            Console.Write("This program will find the mean (average) number of words in songs by an artist you choose.\n");
            Console.Write("Enter an artist name:\n");

            var chosenArtist = Console.ReadLine();

            while (chosenArtist == null)
            {
                Console.WriteLine("You must enter an artist name:\n");
                chosenArtist = Console.ReadLine();
            }

            await TestWordCounter(chosenArtist);
            //await DetermineAverageWords(chosenArtist);
        }

        private static async Task TestWordCounter(string artist)
        {
            Console.WriteLine("Song Title:");
            var title = Console.ReadLine();

            var request = new LyricsRequest{ Artist = artist, Title = title };
            var lyrics = (await Repository.GetLyricsAsync(request)).Lyrics ?? "";
            // Console.WriteLine(lyrics);

            var wordCount = Repository.GetWordCount(lyrics);
            Console.WriteLine($"WordCount: {wordCount}");
        }

        private static async Task DetermineAverageWords(string artist)
        {
            var totalWords = 0;
            var numberOfSongs = 0;

            var allTracks = await Repository.GetAllTracksAsync(artist);

            if (allTracks.Data != null)
            {
                foreach (var track in allTracks.Data)
                {
                    var currentSong = new LyricsRequest
                    {
                        Artist = artist,
                        Title = track.Title
                    };

                    var currentLyrics = await Repository.GetLyricsAsync(currentSong);

                    if (currentLyrics.Lyrics != null)
                    {
                        numberOfSongs += 1;
                        totalWords += Repository.GetWordCount(currentLyrics.Lyrics);
                    }
                }
            }

            if (allTracks.Data != null)
            {
                Console.WriteLine($"The average number of words in a song by {artist} is {totalWords / numberOfSongs}");
            }
        }
    }
}