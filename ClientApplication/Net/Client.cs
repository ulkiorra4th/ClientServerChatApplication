using ClientApplication.CLI;
using ClientApplication.Models;
using ClientApplication.Net.Constants;
using ClientApplication.Net.Interfaces;
using ClientApplication.Net.IO;
using ClientApplication.Net.IO.Interfaces;

namespace ClientApplication.Net;

internal sealed class Client : IClient
{
    private readonly List<UserModel> _users;
    private readonly IConnection _connection;
    private readonly IPacketReader _packetReader;

    public Client(IConnection connection)
    {
        _users = new List<UserModel>();
        _connection = connection;
        _packetReader = new PacketReader(_connection.TcpClient.GetStream());
        
        SendMessageToServer(connection.UserName, OperationCode.NewConnectionCode);
    }
    
    public async Task ReadPacketsAsync()
    {
        await Task.Run(() => ReadPackets());
    }

    public void BuildPackets()
    {
        while (true) 
        { 
            SendMessageToServer(ClientCLI.RequestMessage(), OperationCode.NewMessageCode);
        }
    }
    
    private void ReadPackets()
    {
        while (true)
        {
            var opCode = _packetReader.ReadOperationCode();

            switch (opCode)
            {
                case OperationCode.NewConnectionFromServerCode:
                {
                    var user = new UserModel
                    {
                        UserName = _packetReader.ReadMessage(),
                        UserId = _packetReader.ReadMessage()
                    };

                    if (_users.All(usr => usr.UserId != user.UserId))
                    {
                        _users.Add(user);
                        ClientCLI.DisplayNewConnectionMessage(user.UserName);
                    }

                    break;
                }

                case OperationCode.DisconnectCode:
                {
                    string userId = _packetReader.ReadMessage();
                    var disconnectedUser = _users.FirstOrDefault(user => user.UserId == userId);
                    
                    _users.Remove(disconnectedUser!);
                    
                    var disconnectedUserName = disconnectedUser!.UserName;
                    
                    ClientCLI.DisplayDisconnection(disconnectedUserName);
                    break;
                }

                case OperationCode.NewMessageCode:
                {
                    ClientCLI.DisplayMessage(userName: _packetReader.ReadMessage(),
                        message: _packetReader.ReadMessage());
                    break;
                }
            }
        }
    }
    
    private void SendMessageToServer(string message, OperationCode opCode)
    {
        IPacketBuilder packetBuilder = new PacketBuilder();
        
        packetBuilder.WriteOpCode(opCode);
        packetBuilder.WriteMessage(message);
        
        _connection.TcpClient.Client.Send(packetBuilder.GetPacketBytes());
    }
}