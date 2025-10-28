using Humanizer;

Console.WriteLine($"When is the feast? {DateTime.UtcNow.AddHours(30).Humanize()}");
Console.WriteLine($"When is the feast? {DateTime.UtcNow.AddHours(2.5).Humanize()}");
Console.WriteLine($"When is the feast? {DateTime.UtcNow.AddHours(500).Humanize()}");