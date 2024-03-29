﻿using LyricsAPIClient.Repositories;

namespace LyricsAPIClient
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();

        public static async Task Main(string[] args)
        {
            Console.Write("This program will find the mean (average) number of words in songs by an artist you choose.\n");
            Console.Write("Enter an artist name:\n");

            var artist = Console.ReadLine();

            while (artist == null)
            {
                Console.WriteLine("You must enter an artist name:\n");
                artist = Console.ReadLine();
            }
            
            await DetermineAverageWords(artist);
        }

        private static async Task DetermineAverageWords(string artist)
        {
            var totalWords = 0;
            var numberOfSongs = 0;

            var allTracks = await TracksRepository.GetAllTracksAsync(artist);

            if (allTracks.Data != null)
            {
                foreach (var track in allTracks.Data)
                {
                    var currentSong = new LyricsRequest
                    {
                        Artist = artist,
                        Title = track.Title
                    };

                    var currentLyrics = await TracksRepository.GetLyricsAsync(currentSong);

                    if (currentLyrics.Lyrics != null)
                    {
                        numberOfSongs += 1;
                        totalWords += StringsRepository.GetWordCount(currentLyrics.Lyrics);
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