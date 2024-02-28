using ServerApplication.Entity;
using ServerApplication.Logging;
using ServerApplication.Net.Constants;
using ServerApplication.Net.Interfaces;
using ServerApplication.Net.IO;
using ServerApplication.Net.IO.Interfaces;

namespace ServerApplication.Net;

internal sealed class Broadcast : IBroadcast
{
    private static Broadcast? _instance;
    
    private readonly ILogger<Broadcast> _logger;
    private readonly List<Client> _clients;

    public static Broadcast Instance => _instance ?? (_instance = new Broadcast());
    
    private Broadcast()
    {
        _logger = new Logger<Broadcast>();
        _clients = new List<Client>();
    }
    
    public void BroadcastConnection(Client client)
    {
        _clients.Add(client);

        foreach (var receivingClient in _clients)
        {
            foreach (var sendingClient in _clients)
            {
                IPacketBuilder broadcastPacket = new PacketBuilder();
                
                broadcastPacket.WriteOpCode(OperationCode.NewConnectionFromServerCode); 
                broadcastPacket.WriteMessage(sendingClient.UserName);
                broadcastPacket.WriteMessage(sendingClient.UserId.ToString());

                try
                {
                    receivingClient.ClientSocket.Client.Send(broadcastPacket.GetPacketBytes());
                }
                catch (Exception e)
                {
                    _logger.LogError(e);
                }
            }
        }
    }
    
    public void BroadcastMessage(string userName, string message)
    {
        foreach (var client in _clients)
        {
            IPacketBuilder messagePacket = new PacketBuilder();
            
            messagePacket.WriteOpCode(OperationCode.NewMessageCode);
            messagePacket.WriteMessage(userName);
            messagePacket.WriteMessage(message);

            client.ClientSocket.Client.Send(messagePacket.GetPacketBytes());
        }
    }

    public void BroadcastDisconnect(string userId)
    {
        var disconnectedClient = _clients.FirstOrDefault(client => client.UserId.ToString() == userId);
        _clients.Remove(disconnectedClient!);
        
        foreach (var client in _clients)
        {
            IPacketBuilder messagePacket = new PacketBuilder();
            
            messagePacket.WriteOpCode(OperationCode.DisconnectCode);
            messagePacket.WriteMessage(userId);

            client.ClientSocket.Client.Send(messagePacket.GetPacketBytes());
        }
    }
}