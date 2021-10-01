using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;

    

    namespace ClassLibrary1
    {

        

      
        interface IConected
        {
           IPEndPoint point { get; set; }
           Socket socket { get; set; }
        void Conect();

        }
   
   public class Klients : IConected
    {
        public IPEndPoint point { get; set; }
        public Socket socket { get; set; }
        public Klients(IPEndPoint _point, Socket _socket)
        {
            point = _point;
            socket = _socket;

        }
        public void Conect()
        {
            try
            {
                socket.Connect(point);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }

        }
        public void Massage(string massage)
        {
            try
            {
                byte[] data = Encoding.Unicode.GetBytes(massage);
                socket.Send(data);
                data = new byte[256];
                StringBuilder builder = new StringBuilder();
                int bytes = 0;
                do
                {
                    bytes = socket.Receive(data, data.Length, 0);
                    builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                }
                while (socket.Available > 0);
                Console.WriteLine("Ответ:" + builder.ToString());
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }
    }

}

