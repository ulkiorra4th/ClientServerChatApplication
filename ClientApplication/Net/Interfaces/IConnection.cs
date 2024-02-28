using System.Net.Sockets;

namespace ClientApplication.Net.Interfaces;

internal interface IConnection
{
    public TcpClient TcpClient { get; }
    public string UserName { get; }
    public string HostName { get; }
    public int Port { get; }
    public bool TryConnectToServer(string? userName, string? hostName, int port);
}