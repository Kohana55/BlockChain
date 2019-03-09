using System;
using System.Net;
using System.Net.Sockets;

namespace BlockChain.Models.Networking
{
    public class P2PServer
    {
        public TcpListener server;
        public TcpClient client;
        IPEndPoint ip;

        bool IsConnected { get; set; } = false;
        int Port = 0;
        bool serverListening = false;        

        /// <summary>
        /// Setup the server, scanning for availale port
        /// </summary>
        /// <param name="port"></param>
        public P2PServer(int port)
        {
            Port = port;

            while (serverListening == false)
            {
                try
                {
                    ip = new IPEndPoint(IPAddress.Any, Port);
                    server = new TcpListener(ip);
                    server.Start();
                    serverListening = true;
                }
                catch (Exception ex)
                {
                    Port++;
                }
            }

            ListenForConnection();
        }

        /// <summary>
        /// Blocks Thread using a blocking call "AcceptTcpClient()"
        /// </summary>
        public void ListenForConnection()
        {
            client = server.AcceptTcpClient();
            IsConnected = true;
        }
    }
}
