

// Intro and instructions before the game actually begins, clears for good once game starts.

Game game = new Game();

Console.WriteLine("Welcome to Tic-Tac-Toe! Choose a square by entering a number (1 - 9) on your turn." +
    "\n\nUse the below board as reference:\n");

Console.WriteLine(" 7 | 8 | 9 ");
Console.WriteLine("---+---+---");
Console.WriteLine(" 4 | 5 | 6 ");
Console.WriteLine("---+---+---");
Console.WriteLine(" 1 | 2 | 3 ");

game.FillBoardWithBlanks();

Console.Write("\nPress any key to begin.");
Console.ReadKey();
Console.Clear();


// Loop the game forever, checking for win/draw after every turn.

while (true)
{
    game.turn++;

    game.GetPlayerTurn('X');
    game.CheckForWinner('X');

    game.turn++;

    game.GetPlayerTurn('O');
    game.CheckForWinner('O');
}


// Player class handles user input and feed it to the Game class.

public class Player
{
    public int PlayerChoice { get; private set; }

    public int GetPlayerChoice()
    {
        return PlayerChoice = Convert.ToInt32(Console.ReadLine());
    }
}


// This class takes player input and updates the game board, as well as tracks when the game ends via win or draw.
// Probably should have split this into at least 2 classes (GameState + GameBoard, maybe?) but was getting stuck when attempting to do so.

public class Game
{
    public char[] _box = new char[10];
    public int turn = 0;

    Player player = new Player();

    public void FillBoardWithBlanks()
    {
        for (int index = 0; index < _box.Length; index++)
        {
            _box[index] = ' ';
        }
    }

    public void GetPlayerTurn(char token)
    {
        while (true)
        {
            Console.WriteLine($"\nIt is {token}'s turn.");
            PrintBoard();
            Console.Write("\nChoose a square: ");

            player.GetPlayerChoice();

            Console.Clear();

            if (player.PlayerChoice >= 1
                && player.PlayerChoice <= 9
                && _box[player.PlayerChoice] != 'X'
                && _box[player.PlayerChoice] != 'O')
            {
                _box[player.PlayerChoice] = token;
                break;
            }
            else
            {
                PrintInvalidChoice();
            }
        }
    }

    public void PrintBoard()
    {
        Console.WriteLine($"\n {_box[7]} | {_box[8]} | {_box[9]} ");
        Console.WriteLine($"---+---+---");
        Console.WriteLine($" {_box[4]} | {_box[5]} | {_box[6]} ");
        Console.WriteLine($"---+---+---");
        Console.WriteLine($" {_box[1]} | {_box[2]} | {_box[3]} ");
    }

    public void PrintInvalidChoice()
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Invalid choice. Try again.");
        Console.ForegroundColor = ConsoleColor.Gray;
    }

    public bool CheckForWinner(char token)
    {
        if (_box[7] == token && _box[4] == token && _box[1] == token
            || (_box[8] == token && _box[5] == token && _box[2] == token)
            || (_box[9] == token && _box[6] == token && _box[3] == token)
            || (_box[7] == token && _box[8] == token && _box[9] == token)
            || (_box[4] == token && _box[5] == token && _box[6] == token)
            || (_box[1] == token && _box[2] == token && _box[3] == token)
            || (_box[7] == token && _box[5] == token && _box[3] == token)
            || (_box[1] == token && _box[5] == token && _box[9] == token))
        {
            Console.Write($"{token} wins! Press [ENTER] to play again.\n");
            StartNewGame();
            return true;
        }
        else if (turn == 9)
        {
            Console.Write("It's a draw! Press [ENTER] to play again.\n");
            StartNewGame();
            return true;
        }

        return false;
    }

    public void StartNewGame()
    {
        turn = 0;
        PrintBoard();
        Console.ReadKey();
        FillBoardWithBlanks();
        Console.Clear();
    }
}