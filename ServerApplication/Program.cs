using ServerApplication.Net.Interfaces;
using ServerApplication.CLI;
using ServerApplication.Net;

ServerCLI.DisplayWelcomeMessage();

string? ipAddress;
int port;

while (!ServerCLI.RequestSocket(out ipAddress, out port)) { }

IServer serverApplication = new Server(ipAddress, port);
serverApplication.Run();