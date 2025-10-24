/*
 * Objectives:
 * 
 * [x] Make the method int RandomlyRecreate(string word). It should take the string's length and generate an equal number of random characters.
 *     it is okay to assume all words use only lowercase. One way to randomly generate a lowercase letter is (char)('a' + random.Next(26)).
 *     This method should loop until it randomly generates the target word, counting the required attempts. The return value is the number of attempts.
 * [x] Make the method Task<int> RandomlyRecreateAsync(string word) that schedules the above method to run asynchronously. (Task.Run is one option)
 * [x] Have your main method ask the user for a word. Run the RandomlyRecreateAsync method and await its result and display it. Note: Be careful about
 *     long words. For me, a five letter word took several seconds, and my math indicates that 10 letter word may take nearly two years.
 * [x] Use DateTime.Now before and after the async task runs to measure how long it took. Display the time elapsed.
 * 
 */

Console.Write("Enter a word that is between 1 and 5 letters long... or 6, if you dare: ");
string? word = Console.ReadLine();

DateTime start = DateTime.Now;

if (word != null)
{
    await RandomlyRecreateAsync(word);
}

TimeSpan totalTime = DateTime.Now - start;

Console.WriteLine($"Total runtime: {totalTime.Minutes} minute(s), {totalTime.Seconds} second(s), {totalTime.Milliseconds} millisecond(s).");

Task<int> RandomlyRecreateAsync(string word)
{
    return Task.Run(() => RandomlyRecreate(word));
}

int RandomlyRecreate(string word)
{
    Random random = new Random();
    List<char> letters = new List<char>();
    string generatedWord = "";
    int attempts = 1;

    while (generatedWord != word)
    {
        letters.Clear();
        generatedWord = "";

        for (int index = 0; index < word.Length; index++)
        {
            char letter = (char)('a' + random.Next(26));
            letters.Add(letter);
        }

        foreach (char letter in letters)
        {
            generatedWord += letter.ToString();
        }

        attempts++;
    }

    Console.WriteLine($"It took {attempts} attempts to generate the word: \"{word}\"");
    return attempts;
}