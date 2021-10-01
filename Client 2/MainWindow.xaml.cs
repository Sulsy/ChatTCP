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
using ClassLibrary1;
namespace Client
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static string ip = "127.0.0.1", name, massage;
        static int port = 8005;
        IPEndPoint ipoint = new IPEndPoint(IPAddress.Parse(ip), port);
        Socket lisentsock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        public MainWindow()
        {
            InitializeComponent();
           
           
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
             massage = Convert.ToString(Text);
            name += massage;
            if (massage == null)
                return;
            else
            {
                if (name == null)
                    return;
                else
                {
                    Klients klient = new Klients(ipoint, lisentsock);
                    klient.Conect();
                    klient.Massage(name);
                }
            }
               
        }

        private void Go_Click(object sender, RoutedEventArgs e)
        {
             name = Convert.ToString(Login);
            Server server = new Server(ipoint, lisentsock);
                server.Conect();
                
                   
                
            
            
        }
    }
}
