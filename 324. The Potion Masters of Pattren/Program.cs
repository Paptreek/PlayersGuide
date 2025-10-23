/*
 * 
 * All potions start as water
 * water + stardust           = elixir
 * elixir + snake venom       = poison potion
 * elixir + dragon breath     = flying potion
 * elixir + shadow glass      = invisibility potion
 * elixir + eyeshine gem      = night sight potion
 * night sight + shadow glass = cloudy brew
 * invis + eyeshine           = cloudy brew
 * cloudy brew + stardust     = wraith potion
 * anything else              = ruined potion
 * 
 * 1. create enums for potion and ingredient types
 * 2. tell user what potion they currently have and what ingredient choices are available
 * 3. allow them to enter ingredient choice. use a pattern to turn user response into an ingredient
 * 4. change current potion type using a pattern
 * 5. allow user to complete potion or continue before adding an ingredient
 * 5a. if user chooses to complete potion, end the program
 * 6. when user creates a ruined potion, tell them and start over with water
 * 
 */

Potions currentPotion = Potions.Water;

while (true)
{
    Console.WriteLine($"\nCurrent Potion: {currentPotion}\n");
    Console.Write("What would you like to do? (1=Add Ingredient, 2=Complete Potion): ");
    int choice = Convert.ToInt32(Console.ReadLine());

    if (choice == 1)
    {
        CreatePotion();

        if (currentPotion == Potions.Ruined)
        {
            Console.WriteLine("Oh no! The potion was ruined! Please try again.");
            currentPotion = Potions.Water;
        }
    }
    else
    {
        break;
    }
}

Console.WriteLine($"\nFinal Potion: {currentPotion}");

Potions CreatePotion()
{
    currentPotion = (GetIngredient(), currentPotion) switch
    {
        (Ingredients.Stardust, Potions.Water) => Potions.Elixir,
        (Ingredients.SnakeVenom, Potions.Elixir) => Potions.Poison,
        (Ingredients.DragonsBreath, Potions.Elixir) => Potions.Flying,
        (Ingredients.ShadowGlass, Potions.Elixir) => Potions.Invisibility,
        (Ingredients.EyeshineGem, Potions.Elixir) => Potions.NightSight,
        (Ingredients.ShadowGlass, Potions.NightSight) => Potions.CloudyBrew,
        (Ingredients.EyeshineGem, Potions.Invisibility) => Potions.CloudyBrew,
        (Ingredients.Stardust, Potions.CloudyBrew) => Potions.Wraith,
        _ => Potions.Ruined,
    };

    return currentPotion;
}

Ingredients GetIngredient()
{
    Console.WriteLine($"\nAvailable ingredients:\n");

    Console.WriteLine($"stardust");
    Console.WriteLine($"snake venom");
    Console.WriteLine($"dragons breath");
    Console.WriteLine($"shadow glass");
    Console.WriteLine($"eyeshine gem");

    Console.Write($"\nWhich ingredient would you like to add? ");

    string? choice = Console.ReadLine();

    Ingredients ingredient = choice switch
    {
        "stardust" => Ingredients.Stardust,
        "snake venom" => Ingredients.SnakeVenom,
        "dragons breath" => Ingredients.DragonsBreath,
        "shadow glass" => Ingredients.ShadowGlass,
        "eyeshine gem" => Ingredients.EyeshineGem,
    };

    Console.WriteLine($"You added {choice} into the mix.");
    return ingredient;
}

enum Potions { Water, Elixir, Poison, Flying, Invisibility, NightSight, CloudyBrew, Wraith, Ruined }
enum Ingredients { Stardust, SnakeVenom, DragonsBreath, ShadowGlass, EyeshineGem }