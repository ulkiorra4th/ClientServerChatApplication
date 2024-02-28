namespace ClientApplication.Net.Interfaces;

internal interface IClient
{
    public Task ReadPacketsAsync();
    public void BuildPackets();
}