int manticoreHealth = 10;
int manticoreMaxHealth = 10;
int cityHealth = 15;
int cityMaxHealth = 15;
int round = 1;
int cannonDamage = 1;
int shotCount = 0;
int normalShot = 1;
int elementalDamage = 3;
int megaShot = 10;



// Two players
/*
Console.Write("Player 1 - How far from the city do you want to station the Manticore? (1 - 100): ");
int manticoreLocation = Convert.ToInt32(Console.ReadLine());
Console.Clear();

Console.WriteLine("Player 2 - It is your turn. You must defend the city by destroying the Manticore!\n\n" +
    "A ship called \"The Manticore\" is coming for the city of Consolas.\n" +
    "To hit it with your Magic Cannon, you must find the number between 0 and 100.\n");
*/


// One player

Random random = new Random();
int manticoreLocation = random.Next(1, 101);

Console.WriteLine("You must defend the city by destroying the Manticore!\n\n" +
    "A ship called \"The Manticore\" is coming for the city of Consolas.\n" +
    "To hit it with your Magic Cannon, you must find the number between 0 and 100.\n");


while (manticoreHealth > 0 && cityHealth > 0)
{
    PredictDamage();
    PrintStatusUpdate();
    CannonShot();
    round++;
    cityHealth--;
    shotCount++;

    if (cityHealth <= 0 && manticoreHealth > 0)
    {
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine("The city of Consolas was destroyed. The Manticore reigns supreme!");
        Console.ForegroundColor = ConsoleColor.White;
    }
    else if (manticoreHealth <= 0 && cityHealth > 0)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("The Manticore has been destroyed! You've saved the city of Consolas!");
        Console.ForegroundColor = ConsoleColor.White;

    }
}

// METHODS BELOW

void PrintStatusUpdate()
{
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine("----------------------------------------------------------------\n");
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine($"STATUS: Round: {round} City: {cityHealth}/{cityMaxHealth} HP Manticore: {manticoreHealth}/{manticoreMaxHealth} HP");
    Console.WriteLine($"The cannon is expected to deal {cannonDamage} damage this round.");
}

bool PlayerShot()
{
    string aimHint;

    Console.Write("Enter desired cannon range: ");

    int cannonRange = Convert.ToInt32(Console.ReadLine());

    if (cannonRange < manticoreLocation)
    {
        Console.ForegroundColor = ConsoleColor.DarkRed;
        aimHint = "FELL SHORT of";
        Console.WriteLine($"Your shot {aimHint} the target.\n");
        return false;
    }
    else if (cannonRange > manticoreLocation)
    {
        Console.ForegroundColor = ConsoleColor.DarkRed;
        aimHint = "OVERSHOT";
        Console.WriteLine($"Your shot {aimHint} the target.\n");
        return false;
    }
    else
    {
        return true;
    }
}

bool CannonShot()
{
    if (PlayerShot() == true)
    {
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine("That round was a DIRECT HIT!\n");
        Console.ForegroundColor = ConsoleColor.White;
        DealDamage();
    }
    return true;
}

void DealDamage()
{

    int fireShot = round % 3;
    int electricShot = round % 5;

    // Actual damage calculator

    if (fireShot > 0 && electricShot > 0)
    {
        manticoreHealth = manticoreHealth - normalShot;
    }
    else if (fireShot == 0 && electricShot == 0)
    {
        manticoreHealth = manticoreHealth - megaShot;
    }
    else if (fireShot == 0)
    {
        manticoreHealth = manticoreHealth - elementalDamage;
    }
    else if (electricShot == 0)
    {
        manticoreHealth = manticoreHealth - elementalDamage;
    }

}

void PredictDamage()
{
    int roundCheckThree = round % 3;
    int roundCheckFive = round % 5;

    if (roundCheckThree > 0 && roundCheckFive > 0)
    {
        cannonDamage = normalShot;
    }
    else if (roundCheckThree == 0 && roundCheckFive == 0)
    {
        cannonDamage = megaShot;
    }
    else if (roundCheckThree == 0)
    {
        cannonDamage = elementalDamage;
    }
    else if (roundCheckFive == 0)
    {
        cannonDamage = elementalDamage;
    }
}