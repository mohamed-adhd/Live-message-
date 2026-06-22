using System.Data;
using System.Net.Sockets;
namespace live_message_app.Services;
public class Network
{
    private TcpClient? _client;

    public bool Connect(string ip, int port)
    {
        _client = new TcpClient();
        try
        {
            _client.Connect(ip, port);
            return true;
        }
        catch
        {
            return false;
        }

        return true;
    }
}