
Console.WriteLine("Instructions: Type words below and I will replicate them all using randomly generated letters.\n");

while (true)
{
    string? word = Console.ReadLine();
    Task outputTask = PrintOutput(word);
}

async Task PrintOutput(string? word)
{
    DateTime start = DateTime.Now;

    int attempts = await RandomlyRecreateAsync(word);

    TimeSpan totalTime = DateTime.Now - start;

    Console.WriteLine($"\nIt took {attempts} attempts to generate the word: \"{word}\"");
    Console.WriteLine($"Total runtime: {totalTime.Minutes} minute(s), {totalTime.Seconds} second(s), {totalTime.Milliseconds} millisecond(s).\n");
}

Task<int> RandomlyRecreateAsync(string? word)
{
    return Task.Run(() => RandomlyRecreate(word));
}

int RandomlyRecreate(string? word)
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

    return attempts;
}