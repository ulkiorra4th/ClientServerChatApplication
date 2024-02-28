using System.Text;

namespace ClientApplication.CLI;

internal static class ClientCLI
{
    static ClientCLI()
    {
        Console.InputEncoding = Encoding.UTF8;
        Console.OutputEncoding = Encoding.UTF8;
    }
    
    public static void DisplayWelcomeMessage()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        
        Console.WriteLine("\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
        Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
        Console.WriteLine("~~~Welcome to my Client-Server Chat Application~~~");
        Console.WriteLine("~~~~~Source - https://github.com/ulkiorra4th ~~~~~");
        Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
        Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n");
        
        Console.ResetColor();
    }

    public static (string?, string?, int) RequestUserNameAndSocket()
    {
        Console.Write("Enter your username: ");
        string? userName = Console.ReadLine();
        
        Console.Write("Enter hostname: ");
        string? hostName = Console.ReadLine();
        
        Console.Write("Enter port: ");
        int port = 0;

        try
        {
            port = Convert.ToInt32(Console.ReadLine());
        }
        catch (FormatException)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nPORT MUST BE A NUMBER\n");
            Console.ResetColor();
        }

        
        Console.WriteLine();
        return (userName, hostName, port);
    }

    public static void DisplayNewConnectionMessage(string userName)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"[{DateTime.Now}]: user {userName} has connected");
        Console.ResetColor();
    }

    public static string RequestMessage()
    {
        return Console.ReadLine() ?? "";
    }

    public static void DisplayMessage(string userName, string message)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write($"[{DateTime.Now.ToShortTimeString()}] {userName}: ");
        Console.WriteLine(message);
        Console.ResetColor();
    }

    public static void DisplayErrorMessage(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"\n{message.ToUpper()}\n");
        Console.ResetColor();
    }

    public static void DisplayDisconnection(string userName)
    {
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine($"[{DateTime.Now.ToShortTimeString()}] {userName}: DISCONNECTED");
        Console.ResetColor();
    }
}