using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class SimpleUdpClient
{
    public static void Main()
    {
        Socket client = new Socket(AddressFamily.InterNetwork,
                                    SocketType.Dgram, ProtocolType.Udp);

        EndPoint remoteEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9050);
        //Note -- need to use EndPoint for the ReceiveFrom to work!

        byte[] data;
        string input;
        int recv;


        while (true)
        {
            Console.Write("Enter message for server or exit to stop: ");
            input = Console.ReadLine();
            if (input == "exit")
                break;
            client.SendTo(Encoding.ASCII.GetBytes(input), remoteEP);
            data = new byte[1024];
            recv = client.ReceiveFrom(data, ref remoteEP);
            Console.Write("Echo from from {0}: ", remoteEP.ToString());
            Console.WriteLine(Encoding.ASCII.GetString(data, 0, recv));
        }
        Console.WriteLine("Stopping client");
        client.Close();
    }
}