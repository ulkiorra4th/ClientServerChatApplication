using ClientApplication.Net.Constants;

namespace ClientApplication.Net.IO.Interfaces;

internal interface IPacketReader
{
    public string ReadMessage();
    public OperationCode ReadOperationCode();
}