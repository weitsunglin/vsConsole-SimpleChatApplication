using System;
using System.Collections.Generic;
using System.Text;

using System.Net.Sockets;
using System.Net;


namespace SimpleChatServer
{

    class Program
    {

        static Socket serverSocket, clientSocket;
        static IPEndPoint ipendPoint;
        static IPAddress ipAddress;
        static int port = 88;
        static byte[] dataBuffer = new byte[1024];


        static void Main(string[] args)
        {
            Console.WriteLine("---SimpleChatServer---");
            ASyncServer();
        }

        private static void ASyncServer()
        {
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            ipAddress = IPAddress.Parse("192.168.1.101");
            ipendPoint = new IPEndPoint(ipAddress, port);

            serverSocket.Bind(ipendPoint);
            serverSocket.Listen(0);
            clientSocket = serverSocket.Accept();

            //Sending Message
            string msg = "Hello Client";
            byte[] Byte = Encoding.UTF8.GetBytes(msg);
            clientSocket.Send(Byte);


            //Read Message
            clientSocket.BeginReceive(dataBuffer, 0, 1024, SocketFlags.None, ReceiveCallback, null);

            Console.ReadKey();
            clientSocket.Close();
            serverSocket.Close();

        }

        private static void ReceiveCallback( IAsyncResult ar ) 
        { 
            int count =   clientSocket.EndReceive( ar );
            string msgReceive = Encoding.UTF8.GetString( dataBuffer, 0, count );
            Console.WriteLine(msgReceive);
            clientSocket.BeginReceive(dataBuffer, 0, 1024, SocketFlags.None, ReceiveCallback, null);
        }

    }
}