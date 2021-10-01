using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using ClassLibrary2;


namespace serverwpf
{
    class Program
    {
        static void Main(string[] args)
        {
            string ip = "127.0.0.1";
            int port = 8005;
            try
            {
                IPEndPoint ipoint = new IPEndPoint(IPAddress.Parse(ip), port);
                Socket lisentsock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                Server server = new Server(ipoint, lisentsock);
                server.Conect();
                while (true)
                {
                    server.Lisentwpf();
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }

        }
    }
}
