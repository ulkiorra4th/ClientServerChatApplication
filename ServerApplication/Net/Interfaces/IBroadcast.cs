using ServerApplication.Entity;

namespace ServerApplication.Net.Interfaces;

internal interface IBroadcast
{
    public void BroadcastConnection(Client client);

    public void BroadcastMessage(string userName, string message);

    public void BroadcastDisconnect(string userId);
}