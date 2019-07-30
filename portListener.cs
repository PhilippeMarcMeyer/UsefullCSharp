using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace spy
{
    class Program
    {
        static void Main(string[] args)
        {
            WindowsPrincipal pricipal = new WindowsPrincipal(WindowsIdentity.GetCurrent());
            bool hasAdministrativeRight = pricipal.IsInRole(WindowsBuiltInRole.Administrator);
            if (hasAdministrativeRight == true)
            {
                TcpListener server;
                Int32 port = 80;
                IPAddress localAddr = IPAddress.Parse("192.168.10.15");
                server = new TcpListener(localAddr, port);

                try
                {
                    server.Start();
                }
                catch (SocketException exception)
                {
                    throw new Exception(exception.Message);
                }
                Byte[] bytes = new Byte[256];
                String data = null;
                while (true)
                {
                    Console.Write("Waiting for a connection... ");
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("Connected!");
                    data = null;
                    NetworkStream stream = client.GetStream();
                    int i;
                    while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                        Console.WriteLine("Received: {0}", data);
                        data = data.ToUpper();

                        byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);
                        stream.Write(msg, 0, msg.Length);
                        Console.WriteLine("Sent: {0}", data);
                    }

                    client.Close();
                }
            }
        }
    }
}
