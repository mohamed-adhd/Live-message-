using System;
using System.Data;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.IO;
namespace live_message_app.Services;
using static System.Console;

public class packet
{
    public string Type { get; set; }

    public int From { get; set; }

    public int To { get; set; }

    public string Text { get; set; }
}
public class Network
{
    private TcpClient? _client;

    public bool Connect(string ip, int port)
    {
        try
        {
            var client = new TcpClient();
            client.Connect(ip, port);
            _client = client;
            return true;
        }
        catch
        {
            return false;
        }
    }
    public void sendpacket(packet tempo)
    {
        string json = JsonSerializer.Serialize(tempo) + "\n";
        byte[] data = Encoding.UTF8.GetBytes(json);
        NetworkStream stream = _client.GetStream();
        stream.Write(data);

    }
    public packet start_recieving()
    {
        NetworkStream stream = _client.GetStream();
        using var reader = new StreamReader(stream, Encoding.UTF8, leaveOpen: true);

        string? line = reader.ReadLine();
        Console.WriteLine("packet broadcasted:");
        Console.WriteLine(line);

        return JsonSerializer.Deserialize<packet>(line);
    }
}


