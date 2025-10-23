
Console.WriteLine("Welcome to the Password Validator. These are the rules:\n" +
    "\n- Must be at least 6 characters, but no more than 13." +
    "\n- Must contain at least one uppercase letter." +
    "\n- Must contain at least one lowercase letter." +
    "\n- Must contain at least one number." +
    "\n- Must NOT contain a capital T" +
    "\n- Must NOT contain any ampersands (&)\n");

while (true)
{
    Console.Write("Create a new password: ");
    PasswordValidator password = new PasswordValidator(Console.ReadLine());

    if (password.IsGreaterThanFive()
        && password.IsLessThanFourteen()
        && password.ContainsUpper()
        && password.ContainsLower()
        && password.ContainsNumber()
        && !password.ContainsCapitalT()
        && !password.ContainsAmpersand())
    {
        Console.WriteLine($"\nSUCCESS! Your new password is: {password.GetPassword}\n");
    }
}


public class PasswordValidator
{
    private string _password;

    public PasswordValidator(string password)
    {
        _password = password;
    }

    public string GetPassword => _password;


    // Methods for checking if password requirements are met

    public bool IsGreaterThanFive() // Returns true if password is more than 5 characters long
    {
        if (_password.Length > 5)
        {
            return true;
        }

        Console.WriteLine("Too short!");
        return false;
    }

    public bool IsLessThanFourteen() // Returns true if password is less than 14 characters long
    {
        if (_password.Length < 14)
        {
            return true;
        }

        Console.WriteLine("Too long!");
        return false;
    }

    public bool ContainsUpper() // Returns true if an uppercase letter is found
    {
        foreach (char letter in _password)
        {
            if (char.IsUpper(letter))
            {
                return true;
            }
        }

        Console.WriteLine("Needs an uppercase letter!");
        return false;
    }

    public bool ContainsLower() // Returns true if a lowercase letter is found
    {
        foreach (char letter in _password)
        {
            if (char.IsLower(letter))
            {
                return true;
            }
        }

        Console.WriteLine("Needs a lowercase letter!");
        return false;
    }

    public bool ContainsNumber() // Returns true if a number is found
    {
        foreach (char letter in _password)
        {
            if (char.IsNumber(letter))
            {
                return true;
            }
        }

        Console.WriteLine("Needs a number!");
        return false;
    }

    public bool ContainsCapitalT() // Returns true if a capital T is found
    {
        foreach (char letter in _password)
        {
            if (letter == 'T')
            {
                Console.WriteLine("No capital T's allowed!");
                return true;
            }
        }

        return false;
    }

    public bool ContainsAmpersand() // Returns true is ampersand is found
    {
        foreach (char letter in _password)
        {
            if (letter == '&')
            {
                Console.WriteLine("No ampersands allowed!");
                return true;
            }
        }

        return false;
    }
}