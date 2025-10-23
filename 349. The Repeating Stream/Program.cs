RecentNumbers recentNumbers = new RecentNumbers();
Thread thread = new Thread(GenerateNumber);
thread.Start(recentNumbers);

while (true)
{
    Console.ReadKey(true);

    bool isDuplicate;

    lock (recentNumbers)
    {
        isDuplicate = recentNumbers.RecentNumber == recentNumbers.SecondNumber;
    }

    if (isDuplicate)
    {
        Console.WriteLine("Nice job! The numbers match.");
    }
    else
    {
        Console.WriteLine("Sorry, the numbers do not match.");
    }
}

void GenerateNumber(object? o)
{
    if (o == null || o is not RecentNumbers) return; // this fixed a couple potentital null errors, in conjunction with the null check after 'object' right above

    RecentNumbers recentNumbers = (RecentNumbers)o; // missed this part on my attempt. I believe this is where the 
    Random random = new Random();

    while (true)
    {
        int nextNumber = random.Next(10);

        lock (recentNumbers)
        {
            recentNumbers.SecondNumber = recentNumbers.RecentNumber;
            recentNumbers.RecentNumber = nextNumber;
        }

        Console.WriteLine(nextNumber);

        Thread.Sleep(1000);
    }
}

public class RecentNumbers
{
    public int RecentNumber { get; set; }
    public int SecondNumber { get; set; }
}