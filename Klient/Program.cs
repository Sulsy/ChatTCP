using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using ClassLibrary1;

namespace Klient
{
    class Program
    {
      
        static void Main(string[] args)
        {
            string ip = "127.0.0.1";
            int port = 8005;
            try
            {
                Console.WriteLine("Введите имя");
                string name = Console.ReadLine();
                while (true)
                {
                    Console.WriteLine("Введите Сообщение");
                    string massage = name + ":" + Console.ReadLine();
                    IPEndPoint ipoint = new IPEndPoint(IPAddress.Parse(ip), port);
                    Socket lisentsock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    Klients klient = new Klients(ipoint, lisentsock);
                    klient.Conect();
                    klient.Massage(massage);
                    massage = string.Empty;
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }


        }
    }
}
