using System.Net;
using System.Net.Sockets;

TcpListener server = new TcpListener(IPAddress.Any, 5000);
server.Start();
Console.WriteLine("Server started on port 5000");

while (true)
{
    TcpClient client = server.AcceptTcpClient();

    Console.WriteLine("Client connected");
}