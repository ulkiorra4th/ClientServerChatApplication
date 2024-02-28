using System.Text;
using ServerApplication.CLI;

namespace ServerApplication.Logging;

internal sealed class Logger<T> : ILogger<T>
{
    // Set your directory
    private const string DirectoryPath = "/Users/ulkiorra/RiderProjects/ClientServerChatApplication/ServerApplication/Logs";
    private static string _fileFullPath = null!;
    
    static Logger()
    {
        _fileFullPath = $"{DirectoryPath}/{DateTime.Today.ToShortDateString()}_LOG.txt";
    }

    public Logger()
    {
        if (!Directory.Exists("Logs/")) Directory.CreateDirectory("Logs/");
    }

    public void Log(string message, bool showInConsole = true)
    {
        _fileFullPath = $"{DirectoryPath}/{DateTime.Today.ToShortDateString()}_LOG.txt";

        using (var stream = new StreamWriter(_fileFullPath, append: true, Encoding.UTF8))
        {
            stream.WriteLine($"[{DateTime.Now}]: MESSAGE FROM {typeof(T)}: {message}");
        }
        
        if (showInConsole) ServerCLI.DisplayMessage(message, typeof(T));
    }

    public void LogError(Exception exception, string message)
    {
        _fileFullPath = $"{DirectoryPath}/{DateTime.Today.ToShortDateString()}_LOG.txt";
        
        using (var stream = new StreamWriter(_fileFullPath, append: true, Encoding.UTF8))
        {
            stream.WriteLine($"[{DateTime.Now}]: {exception.GetType().ToString().ToUpper()} FROM {typeof(T)}:");
            stream.WriteLine($"\t\t {exception.Message} ");
            stream.WriteLine($"\t\t STACKTRACE: {exception.StackTrace} ");
            stream.WriteLine($"\t\t MESSAGE: {message}");
        }
        
        ServerCLI.DisplayError(exception, message, typeof(T));
    }
    
    public void LogError(Exception exception)
    {
        _fileFullPath = $"{DirectoryPath}/{DateTime.Today.ToShortDateString()}_LOG.txt";
        
        using (var stream = new StreamWriter(_fileFullPath, append: true, Encoding.UTF8))
        {
            stream.WriteLine($"[{DateTime.Now}]: {exception.GetType().ToString().ToUpper()} FROM {typeof(T)}:");
            stream.WriteLine($"\t\t {exception.Message} ");
            stream.WriteLine($"\t\t STACKTRACE: {exception.StackTrace} ");
        }
        
        ServerCLI.DisplayError(exception, typeof(T));
    }

    public void LogNewConnection(string userName)
    {
        _fileFullPath = $"{DirectoryPath}/{DateTime.Today.ToShortDateString()}_LOG.txt";
        
        using (var stream = new StreamWriter(_fileFullPath, append: true, Encoding.UTF8))
        {
            stream.WriteLine($"[{DateTime.Now}]: NEW CONNECTION FROM {typeof(T)}: user {userName} has connected");
        }
        
        ServerCLI.DisplayNewConnection(userName);
    }

    public void LogMessage(string userName, string message)
    {
        _fileFullPath = $"{DirectoryPath}/{DateTime.Today.ToShortDateString()}_LOG.txt";
        
        using (var stream = new StreamWriter(_fileFullPath, append: true, Encoding.UTF8))
        {
            stream.WriteLine($"[{DateTime.Now} - {userName}]: {message}");
        }
        
        ServerCLI.DisplayClientsMessage(userName, message);
    }

    public void LogDisconnection(string userName)
    {
        _fileFullPath = $"{DirectoryPath}/{DateTime.Today.ToShortDateString()}_LOG.txt";
        
        using (var stream = new StreamWriter(_fileFullPath, append: true, Encoding.UTF8))
        {
            stream.WriteLine($"[{DateTime.Now}]: DISCONNECTION FROM {typeof(T)}: user {userName} has disconnected");
        }
        
        ServerCLI.DisplayDisconnection(userName);
    }
}
