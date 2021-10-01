using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;
namespace ClassLibrary2
{
    static public class Write
    {

        static public void Records(string log)
        {

            FileStream fileStream = new FileStream(@"Datalog.scv", FileMode.Append);
  
            byte[] array = System.Text.Encoding.Default.GetBytes(log);
            fileStream.Write(array, 0, array.Length);
            fileStream.Close();
        }
    }
    interface IConected
    {
        IPEndPoint point { get; set; }
        Socket socket { get; set; }
        void Conect();

    }
    public class Server : IConected
    {
        public IPEndPoint point { get; set; }
        public Socket socket { get; set; }
        public string _massage { get; set; }
        public Server(IPEndPoint _point, Socket _socket)
        {
            point = _point;
            socket = _socket;

        }
        public void Conect()
        {
            try
            {
                socket.Bind(point);
                socket.Listen(10);
                //Console.WriteLine("Старт");
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }

        }
        public void Lisent(bool variants = true)
        {
            try
            {
                while (variants)
                {
                    Socket handler = socket.Accept();
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0;
                    byte[] data = new byte[256];
                    do
                    {
                        bytes = handler.Receive(data);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (handler.Available > 0);
                    string mas = DateTime.Now.ToLongTimeString() + ": " + builder.ToString();
                    Console.WriteLine(mas);
                    string massage = "Сообщение дошло";
                    _massage = mas;
                    
                    Write.Records(mas + "\n");
                    data = Encoding.Unicode.GetBytes(massage);
                    handler.Send(data);
                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }

        }
        public static string massa = string.Empty;
        public void Lisentwpf(bool variants = true)
        {
            try
            {
                while (true)
                {
                 
                    Socket handler = socket.Accept();
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0;
                    byte[] data = new byte[256];
                  
                        do
                        {
                            bytes = handler.Receive(data);
                            builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                        }
                        while (handler.Available > 0);
                    if (data[0] != 1)
                    {
                        string mab = builder.ToString();
                        mab = mab.Replace(":", ";");
                        string mabs = DateTime.Now.ToLongTimeString() + "; " + mab;
                        string mas = DateTime.Now.ToLongTimeString() + "; " + builder.ToString();
                        Console.WriteLine(mas);
                        Write.Records(mabs + "\n");
                        massa += "\n" + mas;
                        data = Encoding.Unicode.GetBytes(massa);

                        handler.Send(data);
                        handler.Shutdown(SocketShutdown.Both);
                        handler.Close();
                    }
                    else
                    {
                        data = Encoding.Unicode.GetBytes(massa);
                        handler.Send(data);
                        handler.Shutdown(SocketShutdown.Both);
                        handler.Close();
                    }
                  
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }

        }

    }
}
