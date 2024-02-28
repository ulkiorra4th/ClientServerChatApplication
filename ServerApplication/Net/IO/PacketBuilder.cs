using System.Text;
using ServerApplication.Net.Constants;
using ServerApplication.Net.IO.Interfaces;

namespace ServerApplication.Net.IO;

internal sealed class PacketBuilder : IPacketBuilder
{
    private readonly MemoryStream _memoryStream;
    
    public PacketBuilder()
    {
        _memoryStream = new MemoryStream();
    }

    public void WriteOpCode(OperationCode opCode)
    {
        _memoryStream.WriteByte((byte)opCode);
    }

    public void WriteMessage(string message)
    {
        _memoryStream.Write(BitConverter.GetBytes(message.Length));
        _memoryStream.Write(Encoding.ASCII.GetBytes(message));
    }

    public byte[] GetPacketBytes()
    {
        return _memoryStream.ToArray();
    }
}