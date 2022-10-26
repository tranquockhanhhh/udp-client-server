using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class UdpSocketServer
{
    public static void Main()
    {
        IPEndPoint localEP = new IPEndPoint(IPAddress.Any, 9050);

        Socket server = new Socket(AddressFamily.InterNetwork,
                 SocketType.Dgram, ProtocolType.Udp);

        server.Bind(localEP);
        Console.WriteLine("Waiting for a client...");

        //dummy end-point
        EndPoint remoteEP = new IPEndPoint(IPAddress.Any, 0);

        int recv;
        byte[] data;

        while (true)
        {
            data = new byte[1024];
            recv = server.ReceiveFrom(data, ref remoteEP);
            Console.Write("Received from {0}: ", remoteEP.ToString());
            Console.WriteLine(Encoding.ASCII.GetString(data, 0, recv));
            server.SendTo(data, recv, SocketFlags.None, remoteEP);
        }
    }
}