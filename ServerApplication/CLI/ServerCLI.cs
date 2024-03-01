using System.Net;
using System.Text;

namespace ServerApplication.CLI;

internal static class ServerCLI
{
    static ServerCLI()
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

    public static bool RequestSocket(out string? ipAddress, out int port)
    {
        Console.Write("\nEnter IP: ");
        ipAddress = Console.ReadLine();
        
        Console.Write("Enter port: ");
        
        try
        {
            port = Convert.ToInt32(Console.ReadLine());
        }
        catch (FormatException)
        {
            port = 0;
            return false;
        }
        
        return true;
    }

    public static void DisplayServerRunMessage(string ipAddress, int port)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("\n\n~~~~~~~Server has been successfully started!~~~~~~\n\n");
        
        Console.ForegroundColor = ConsoleColor.DarkBlue;
        Console.Write("hostname: ");
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine(ipAddress);
        
        Console.ForegroundColor = ConsoleColor.DarkBlue;
        Console.Write("port: ");
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine(port);
        Console.WriteLine("\n");
        
        Console.ResetColor();
    }

    public static void DisplayClientsMessage(string userName, string message)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write($"[{DateTime.Now.ToShortTimeString()}] {userName}: ");
        Console.WriteLine(message);
        Console.ResetColor();
    }

    public static void DisplayNewConnection(string userName)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"[{DateTime.Now}]: NEW CONNECTION: user {userName} has connected");
        Console.ResetColor();
    }

    public static void DisplayError(Exception exception, Type type)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"[{DateTime.Now}]: {exception.GetType().ToString().ToUpper()} FROM {type}: {exception.Message}");
        Console.ResetColor();
    }
    
    public static void DisplayError(Exception exception, string message, Type type)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"[{DateTime.Now}]: {exception.GetType().ToString().ToUpper()} FROM {type}: {message}");
        Console.ResetColor();
    }

    public static void DisplayMessage(string message, Type sender)
    {
        Console.WriteLine($"[{DateTime.Now}]: {sender}: {message}");
    }

    public static void DisplayDisconnection(string userName)
    {
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine($"[{DateTime.Now}]: DISCONNECTION: user {userName} has disconnected");
        Console.ResetColor();
    }
}