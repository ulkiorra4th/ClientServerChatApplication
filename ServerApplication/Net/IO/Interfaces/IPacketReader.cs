using ServerApplication.Net.Constants;

namespace ServerApplication.Net.IO.Interfaces;

internal interface IPacketReader
{
    public string ReadMessage();
    public OperationCode ReadOperationCode();
}