Console.Write("Enter the initial passcode: ");
Door door = new Door(Convert.ToInt32(Console.ReadLine()));
door.CloseDoor();

while (true)
{
    string choice = GetString($"The door is {door.State}. What would you like to do? ({door.Choices}):");

    switch (choice)
    {
        case "open":
            door.OpenDoor();
            break;
        case "close":
            door.CloseDoor();
            break;
        case "lock":
            door.LockDoor();
            break;
        case "unlock":
            int passcode = GetInt("Enter passcode:");
            door.UnlockDoor(passcode);
            break;
        case "change code":
            int currentPasscode = GetInt("Enter current passcode:");
            int newPasscode = GetInt("Enter new passcode:");
            door.ChangeCode(currentPasscode, newPasscode);
            break;
        default:
            door.InvalidChoice();
            break;
    }
}

string GetString(string text)
{
    Console.Write(text + " ");
    return Console.ReadLine();
}

int GetInt(string text)
{
    Console.Write(text + " ");
    return Convert.ToInt32(Console.ReadLine());
}

public class Door
{
    public int Passcode { get; private set; }
    public DoorState State { get; private set; }
    public string Choices { get; private set; }

    public Door(int passcode)
    {
        Passcode = passcode;
    }

    public void OpenDoor()
    {
        if (State == DoorState.Closed || State == DoorState.Unlocked)
        {
            State = DoorState.Open;
            Choices = "close, change code";
        }
        else
        {
            InvalidChoice();
        }
    }

    public void CloseDoor()
    {
        if (State == DoorState.Open)
        {
            State = DoorState.Closed;
            Choices = "open, lock, change code";
        }
        else
        {
            InvalidChoice();
        }
    }

    public void LockDoor()
    {
        if (State == DoorState.Closed || State == DoorState.Unlocked)
        {
            State = DoorState.Locked;
            Choices = "unlock, change code";
        }
        else
        {
            InvalidChoice();
        }
    }

    public void UnlockDoor(int passcode)
    {
        if (State == DoorState.Locked && passcode == Passcode)
        {
            State = DoorState.Unlocked;
            Choices = "open, lock, change code";
        }
        else
        {
            InvalidChoice();
        }
    }

    public void ChangeCode(int passcode, int newPasscode)
    {
        if (passcode == Passcode)
        {
            Passcode = newPasscode;
            Console.WriteLine($"Success. The passcode is now: {Passcode}");
        }
        else
        {
            InvalidChoice();
        }
    }

    public void InvalidChoice()
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Invalid. Please try again.");
        Console.ForegroundColor = ConsoleColor.Gray;
    }
}

public enum DoorState { Open, Closed, Locked, Unlocked }