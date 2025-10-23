/*
 * Challenge Overview:
 * 
 * Array: 1, 9, 2, 8, 3, 7, 4, 6, 5
 * 
 * All the odd numbers filtered out, only even numbers considered
 * The numbers in order
 * The numbers doubled
 * 
 * First: 2, 8, 4, 6
 * Second: 2, 4, 6, 8
 * Third: 4, 8, 12, 16
 * 
 * Objectives:
 * 
 * 1. Write a method that takes an int[] as input and produce an IEnumerable<int> (could be a list or array if you want) that meets all three conditions above
 * using only procedural code -- if statements, switches, and loops. Hint: The static Array.Sort method might be useful here
 * 
 * 2. Write a method that takes an int[] as an input and produce an IEnumerable<int> that meets the three above conditions using a keyword-based query expression
 * (from x, where x, select x, etc)
 * 
 * 3. Write another using a method-call-based query expression. (x.Select(n => n + 1), x.Where(n => n < 0), etc)
 * 
 * 4. Run all three methods and display the results to ensure they all produce good answers
 * 
 * Question 1: Compare these 3 methods. Do any stand out as being particularly good or bad?
 * Answer: I wouldn't say any are bad, per se, but using query expressions is certainly easier to understand and to implement.
 * 
 * Question 2: Of the three, which is your personal favorite, and why?
 * Answer: I liked keyword queries, at least for this exercise, because they are the cleanest and most straightforward.
 * 
 */

int[] array = new int[] { 1, 9, 2, 8, 3, 7, 4, 6, 5 };

foreach (int number in ProceduralCode(array))
{
    Console.Write($"{number} ");
}

Console.WriteLine();

foreach (int number in KeywordQuery(array))
{
    Console.Write($"{number} ");
}

Console.WriteLine();

foreach (int number in MethodCallQuery(array))
{
    Console.Write($"{number} ");
}

IEnumerable<int> ProceduralCode(int[] array)
{
    List<int> filtered = new List<int>();

    foreach (int number in array)
    {
        if (number % 2 == 0)
        {
            filtered.Add(number);
        }
        ;
    }

    int[] result = filtered.ToArray();
    Array.Sort(result);

    for (int index = 0; index < result.Length; index++)
    {
        result[index] *= 2;
    }

    return result;
}

IEnumerable<int> KeywordQuery(int[] array)
{
    return from a in array
           where a % 2 == 0
           orderby a
           select a * 2;
}

IEnumerable<int> MethodCallQuery(int[] array)
{
    return array
        .Where(a => a % 2 == 0)
        .OrderBy(a => a)
        .Select(a => a * 2);
}