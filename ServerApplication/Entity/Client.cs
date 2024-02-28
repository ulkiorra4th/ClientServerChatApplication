using System.Net.Sockets;
using ServerApplication.Logging;
using ServerApplication.Net;
using ServerApplication.Net.Constants;
using ServerApplication.Net.Interfaces;
using ServerApplication.Net.IO;
using ServerApplication.Net.IO.Interfaces;

namespace ServerApplication.Entity;

internal sealed class Client
{
    private readonly IPacketReader _packetReader;
    private readonly ILogger<Client> _logger;
    private readonly IBroadcast _broadcast;
    
    public string UserName { get; }
    public Guid UserId { get; }
    public TcpClient ClientSocket { get; }

    public Client(TcpClient clientSocket)
    {
        _packetReader = new PacketReader(clientSocket.GetStream());
        _packetReader.ReadOperationCode();
        _logger = new Logger<Client>();
        _broadcast = Broadcast.Instance;

        UserName = _packetReader.ReadMessage();
        UserId = Guid.NewGuid();
        ClientSocket = clientSocket;
        
        Task.Run(() => Process());
    }

    private void Process()
    {
        while (true)
        {
            try
            {
                var opCode = _packetReader.ReadOperationCode();

                switch (opCode)
                {
                    case OperationCode.NewMessageCode:
                    {
                        string message = _packetReader.ReadMessage();

                        _logger.LogMessage(UserName, message);
                        _broadcast.BroadcastMessage(UserName, message);
                        break;
                    }

                    case OperationCode.ErrorCode:
                    {
                        _logger.LogError(new FormatException(), "undefined operation code");
                        break;
                    }
                }
            }
            catch
            {
                ClientSocket.Close();
                _broadcast.BroadcastDisconnect(UserId.ToString());
                _logger.LogDisconnection(UserName);
                return;
            }
        }
    }
}