# LyricsAPIClient
## Required packages:
Netwonsoft.Json

## Instructions
The program will begin after you enter the 'dotnet run' command. You will be asked to enter an artist's name; if you do not enter a name you will be asked again until a name has been entered.

## Self-reflection / Future Improvements
 - With more time I could have worked on a method to remove the first line from the lyrics.ovh response which is the title of the song, so that the program is more accurate.
 - I could have looked more into API request retries so that I could add a step in the code which retries API calls that do not return a Success status code (e.g. retry 3 times before moving onto the next song) to account for any situations where the request might fail due to timeout or because of the calls limit.
 - I could have looked into Unit Testing and added unit tests to make sure that the returns from each method were as expected, since that would have been a good opportunity for me to pick up skills in an area I am less confident in too.
