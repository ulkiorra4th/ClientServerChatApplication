using System.Net.Sockets;
using ClientApplication.CLI;
using ClientApplication.Net.Interfaces;

namespace ClientApplication.Net;

internal sealed class Connection : IConnection
{
    public TcpClient TcpClient { get; }
    public string UserName { get; private set; } = null!;
    public string HostName { get; private set; } = null!;
    public int Port { get; private set; }

    public Connection()
    {
        TcpClient = new TcpClient();
    }

    public bool TryConnectToServer(string? userName, string? hostName, int port)
    {
        if (TcpClient.Connected) return true;
        if (String.IsNullOrEmpty(userName) || String.IsNullOrEmpty(hostName)) return false;
        
        try
        {
            TcpClient.Connect(hostName, port);
        }
        catch (SocketException e)
        {
            ClientCLI.DisplayErrorMessage(e.Message);
            return false;
        }

        UserName = userName;
        HostName = hostName;
        Port = port;
        
        return true;
    }
}
