using System.Net.Sockets;
using System.Text;
using ServerApplication.Logging;
using ServerApplication.Net.Constants;
using ServerApplication.Net.IO.Interfaces;

namespace ServerApplication.Net.IO;

internal sealed class PacketReader : BinaryReader, IPacketReader
{
    private readonly NetworkStream _networkStream;
    private readonly ILogger<PacketBuilder> _logger;

    public PacketReader(NetworkStream input) : base(input)
    {
        _networkStream = input;
        _logger = new Logger<PacketBuilder>();
    }

    public string ReadMessage()
    {
        int length = ReadInt32();
        byte[] messageBuffer = new byte[length];

        _networkStream.Read(messageBuffer, 0, length);

        return Encoding.ASCII.GetString(messageBuffer);
    }

    public OperationCode ReadOperationCode()
    {
        try
        {
            return (OperationCode)ReadByte();
        }
        catch (FormatException e)
        {
            _logger.LogError(e);
            return OperationCode.ErrorCode;
        }
    }
}