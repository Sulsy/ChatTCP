using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibrary2;
using System.IO;
namespace UnitTestProject1
{
    [TestClass]

    public class UnitTest1
    {
        static string ip = "127.0.0.1";
        static string ipExp = "127.a.1";
        string name = "Test";
        static int port = 8005;
        IPEndPoint ipoint = new IPEndPoint(IPAddress.Parse(ip), port);
        Socket lisentsock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        
        [TestMethod]
        public void TestConect_NoExp()
        {
            //arange
            //act
            Server server = new Server(ipoint, lisentsock);
            server.Conect();
            //assert   
        }
        [ExpectedException(typeof(FormatException))]
        [TestMethod]
        public void TestConect_Exp()
        {
            //arange
            IPEndPoint ipointExp = new IPEndPoint(IPAddress.Parse(ipExp), port);
            //act
            Server server = new Server(ipointExp, lisentsock);
            server.Conect();
            //assert   
        }
        [TestMethod]
        public void TestWrite()
        {
            //arange
            string text;
            //act
            Write.Records(name);
            FileStream file = new FileStream(@"Datalog.scv", FileMode.Open);
            byte[] array = new byte[file.Length];
            file.Read(array, 0, array.Length);
            text = System.Text.Encoding.Default.GetString(array);
            //assert   
            Assert.AreEqual(text,name);
        }
        [ExpectedException(typeof(System.IO.FileNotFoundException))]
        [TestMethod]
        public void TestWriteExp()
        {
            //arange
            string text;
            //act
            Write.Records(name);
            FileStream file = new FileStream(@"expkek.scv", FileMode.Open);
            byte[] array = new byte[file.Length];
            file.Read(array, 0, array.Length);
            text = System.Text.Encoding.Default.GetString(array);
            //assert   
            Assert.AreEqual(text, name);
        }



    }
}
