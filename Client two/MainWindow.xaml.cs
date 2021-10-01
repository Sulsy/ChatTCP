using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Windows.Threading;
using ClassLibrary1;

namespace Client_two
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       
        static string ip = "127.0.0.1";
        static string massage{get;set;}
        static string name { get; set; }
        static int port = 8005;
       

        public MainWindow()
        {
            InitializeComponent();
            Start();
        }
        private DispatcherTimer timer = null;
        private void Start()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(2);
            timer.Tick += Upd;
            timer.Start();
        }
        void gocon(byte[] data,IPEndPoint point, Socket socket)
        {
            
           
            try
            {
                
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
                string mas = builder.ToString();
                Console.WriteLine("Ответ:" + mas);
                Ecran.Content = mas;
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }
         void Upd(object sender,EventArgs e)
        {
            IPEndPoint point = new IPEndPoint(IPAddress.Parse(ip), port);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(point);
            byte[] data= new byte[1];
            data[0] = 1;
            gocon(data, point, socket);
        }
       

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
           
            IPEndPoint point = new IPEndPoint(IPAddress.Parse(ip), port);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            massage = Convert.ToString(Text.Text);
            name = Convert.ToString(Login.Text) + ":";
            name += massage;
            if (massage == null)
                return;
            else
            {
                if (name == null)
                    return;
                else
                {
                    socket.Connect(point);
                    try
                    {
                        byte[] data = Encoding.Unicode.GetBytes(name);
                        gocon(data, point, socket);
                        /*socket.Send(data);
                       
                        data = new byte[256];
                        StringBuilder builder = new StringBuilder();
                        int bytes = 0;
                        do
                        {
                            bytes = socket.Receive(data, data.Length, 0);
                            builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                        }
                        while (socket.Available > 0);
                        string mas = builder.ToString();
                        Console.WriteLine("Ответ:" + mas);
                        Ecran.Content = mas;
                        socket.Shutdown(SocketShutdown.Both);
                        socket.Close();*/
                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine(ex.Message);
                    }

                }
            }
            Text.Text = "";

        }





    }

}
