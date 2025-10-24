/*
 * Objectives:
 * 
 * [ ] Modify your previous program to allow the main thread to keep waiting for the user to enter more words. For every new word entered, create a run a task to compute
 *     the attempt count and the time elapsed and display the result, but then let that run asynchronously while you wait for the next word. You can generate many words
 *     in parallel this way.
 *     
 *     Hint: Moving the elapsed time and the output logic to another async method may make this easier.
 * 
 */

while (true)
{
    Console.Write("Enter a word that is between 1 and 5 letters long... or 6, if you dare: ");
    string? word = Console.ReadLine();

    DateTime start = DateTime.Now;

    if (word != null)
    {
        await RandomlyRecreateAsync(word);
    }

    TimeSpan totalTime = DateTime.Now - start;

    Console.WriteLine($"Total runtime: {totalTime.Minutes} minute(s), {totalTime.Seconds} second(s), {totalTime.Milliseconds} millisecond(s).\n");
}

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