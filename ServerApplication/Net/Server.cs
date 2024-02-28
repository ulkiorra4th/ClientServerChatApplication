using System.Net;
using System.Net.Sockets;
using ServerApplication.CLI;
using ServerApplication.Entity;
using ServerApplication.Logging;
using ServerApplication.Net.Interfaces;

namespace ServerApplication.Net;

internal sealed class Server : IServer
{
    private readonly ILogger<Server> _logger;
    private readonly IBroadcast _broadcast;
    private readonly TcpListener? _listener;

    private readonly string _ipAddress;
    private readonly int _port;
    
    public Server(string ipAddress, int port)
    {
        _ipAddress = ipAddress;
        _port = port;

        _logger = new Logger<Server>();
        _broadcast = Broadcast.Instance;
        _listener = new TcpListener(IPAddress.Parse(_ipAddress), _port);
    }
    
    public void Run()
    {
        _listener!.Start();
        _logger.Log("Server has been successfully started", showInConsole: false);
        
        ServerCLI.DisplayServerRunMessage(_ipAddress, _port);

        while (true)
        {
            var client = new Client(_listener.AcceptTcpClient());
            
            _logger.LogNewConnection(client.UserName);
            _broadcast.BroadcastConnection(client);
        }
    }
}