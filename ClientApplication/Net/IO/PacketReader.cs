using System.Net.Sockets;
using System.Text;
using ClientApplication.CLI;
using ClientApplication.Net.Constants;
using ClientApplication.Net.IO.Interfaces;

namespace ClientApplication.Net.IO;

internal sealed class PacketReader : BinaryReader, IPacketReader
{
    private readonly NetworkStream _networkStream;
    
    public PacketReader(NetworkStream input) : base(input)
    {
        _networkStream = input;
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
            ClientCLI.DisplayErrorMessage(e.Message);
            return OperationCode.ErrorCode;
        }
    }
}