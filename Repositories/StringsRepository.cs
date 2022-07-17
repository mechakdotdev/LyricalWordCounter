using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LyricsAPIClient.Repositories
{
    public class StringsRepository
    {
        public static int GetWordCount(string chosenString)
        {
            int count = 0, i = 0;
            bool isWord = false;

            while (i < chosenString.Length)
            {
                if (char.IsWhiteSpace(chosenString[i]) || chosenString[i] == '\n' || chosenString[i] == '\t') {
                    isWord = false;
                }
                else if (isWord == false) {
                    isWord = true;
                    count++;
                }

                i++;
            }

            return count;
        }
    }
}