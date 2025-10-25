
// [ ] Make a single Add method that can replace all 4 of the other Add methods.
// [ ] In main method, call the new method with each data type and display the results.

// Question: What downside do you see with using dynamic here?
// Answer: The compiler doesn't know if the two provided types can be added together, leading to a crash if the two types can't.

Add(1, 2);
Add(1.5, 2.5);
Add("hello", "goodbye");
Add(DateTime.Now, TimeSpan.FromDays(1));

void Add(dynamic a, dynamic b)
{
    Console.WriteLine(a + b);
}

public static class Adds
{
    public static int Add(int a, int b) => a + b;
    public static double Add(double a, double b) => a + b;
    public static string Add(string a, string b) => a + b;
    public static DateTime Add(DateTime a, TimeSpan b) => a + b;
}