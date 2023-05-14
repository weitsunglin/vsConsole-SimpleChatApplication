using System;
using System.Collections.Generic;
using System.Text;

using System.Net.Sockets;
using System.Net;

namespace SimpleChatClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("---SimpleChatClient---");

            Socket clientSocket = new Socket( AddressFamily.InterNetwork , SocketType.Stream, ProtocolType.Tcp );
            clientSocket.Connect( new IPEndPoint(  IPAddress.Parse("192.168.1.101"), 88 ));

            //Reading the Hello Message
            byte[] data = new byte[1024];
            int count = clientSocket.Receive(data);
            string msgReceive = Encoding.UTF8.GetString(data,0,count);
            Console.WriteLine(msgReceive);

            //Sending message to the Server

            while (true)
            {
                string msgSend = Console.ReadLine();
                clientSocket.Send(Encoding.UTF8.GetBytes( msgSend ));
            }

            Console.ReadLine();
            clientSocket.Close();
        }
    }
}