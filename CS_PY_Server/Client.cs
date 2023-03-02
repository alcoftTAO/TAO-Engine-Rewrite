using System;
using System.Net;
using System.Net.Sockets;

namespace TAO.Engine.CS_PY_Server
{
    public static class Client
    {
        public static Socket ClientSocket = null;

        public static void ConnectToServer(int Port = 5325)
        {
            DisconnectFromServer();

            ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            ClientSocket.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), Port));
        }

        public static void DisconnectFromServer()
        {
            if (ClientSocket != null)
            {
                if (ClientSocket.Connected)
                {
                    ClientSocket.Disconnect(false);
                }

                ClientSocket.Close();
                ClientSocket = null;
            }
        }
    }
}