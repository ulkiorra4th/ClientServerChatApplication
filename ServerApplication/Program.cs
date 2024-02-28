using ServerApplication.Net.Interfaces;
using ServerApplication.CLI;
using ServerApplication.Net;

ServerCLI.DisplayWelcomeMessage();

string ipAddress = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList[3].MapToIPv4().ToString();
int port;

while (!ServerCLI.RequestPort(out port)) { }

IServer serverApplication = new Server(ipAddress, port);
serverApplication.Run();