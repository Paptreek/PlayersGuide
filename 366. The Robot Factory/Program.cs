using System.Dynamic;

int robotID = 1;

while (true)
{
    dynamic robot = new ExpandoObject();
    string userInput;
    robot.ID = robotID;
    robotID++;

    userInput = GetUserInput("Do you want to name the robot?");

    if (userInput == "yes")
    {
        robot.Name = GetUserInput("Please enter the robot's name:");
    }

    userInput = GetUserInput("Do you want to choose the robot's size?");

    if (userInput == "yes")
    {
        robot.Width = GetUserInput("Enter the width:");
        robot.Height = GetUserInput("Enter the height:");
    }

    userInput = GetUserInput("Do you want to choose the robot's color?");

    if (userInput == "yes")
    {
        robot.Color = GetUserInput("Enter the color:");
    }

    Console.WriteLine();

    foreach (KeyValuePair<string, object> property in (IDictionary<string, object>)robot)
        Console.WriteLine($"{property.Key}: {property.Value}");

    Console.WriteLine();
}

static string GetUserInput(string? text)
{
    Console.Write(text + " ");
    string? choice = Console.ReadLine();
    return choice;
}