namespace ServerApplication.Logging;

internal interface ILogger<T>
{
    public void Log(string message, bool showInConsole = true);

    public void LogError(Exception exception, string message);

    public void LogError(Exception exception);
    
    public void LogNewConnection(string userName);

    public void LogMessage(string userName, string message);

    public void LogDisconnection(string userName);
}