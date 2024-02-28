using ClientApplication.CLI;
using ClientApplication.Net;
using ClientApplication.Net.Interfaces;

ClientCLI.DisplayWelcomeMessage();

IConnection connection = new Connection();

string? userName;
string? hostName;
int port;

do
{
    (userName, hostName, port) = ClientCLI.RequestUserNameAndSocket();
} while (!connection.TryConnectToServer(userName, hostName, port));

IClient client = new Client(connection);

client.ReadPacketsAsync();
client.BuildPackets();