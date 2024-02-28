using ClientApplication.Net.Constants;

namespace ClientApplication.Net.IO.Interfaces;

internal interface IPacketBuilder
{
    public void WriteOpCode(OperationCode opCode);
    public void WriteMessage(string message);
    public byte[] GetPacketBytes();
}