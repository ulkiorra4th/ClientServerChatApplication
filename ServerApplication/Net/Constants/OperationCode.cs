namespace ServerApplication.Net.Constants;

internal enum OperationCode
{
    NewConnectionCode = 0,
    NewConnectionFromServerCode = 1,
    NewMessageCode = 5,
    ErrorCode = 9,
    DisconnectCode = 10
}