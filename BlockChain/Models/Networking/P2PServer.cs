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
        }


        /// <summary>
        /// Start the TCP Server
        /// </summary>
        public void Start()
        {
            while (serverListening == false)
            {
                try
                {
                    ip = new IPEndPoint(IPAddress.Any, Port);
                    server = new TcpListener(ip);
                    server.Start();
                    serverListening = true;
                    OnPortOpen?.Invoke();
                }
                catch (Exception ex) { Port++; }
            }
            ListenForConnection();
        }

        /// <summary>
        /// Blocks Thread using a blocking call "AcceptTcpClient()"
        /// </summary>
        public void ListenForConnection()
        {
            client = server.AcceptTcpClient();
            OnConnectionSuccessful?.Invoke();
            IsConnected = true;
            StartReceiving();
        }

        /// <summary>
        /// The receiving method - runs until connection is lost (or should)
        /// </summary>
        public void StartReceiving()
        {
            Byte[] buffer = new Byte[256];
            while (client.Connected)
            {
                int bytesRead;
                while((bytesRead = client.GetStream().Read(buffer, 0, buffer.Length)) != 0)
                {
                    // Convert message into ASCII
                    string data = System.Text.Encoding.ASCII.GetString(buffer, 0, bytesRead);

                    // Do stuff with received buffer


                    // Let calling programme know a message was received
                    OnMessageReceived?.Invoke();
                }
            }
        }

        /// <summary>
        /// Writes bytes to the TCP Networking stream
        /// </summary>
        public void Send()
        {
            Byte[] buffer = new Byte[256];
            client.GetStream().Write(buffer, 0, buffer.Length);
        }


        public delegate void OpenPortEventHandler();
        public event OpenPortEventHandler OnPortOpen;

        public delegate void ConnectionSuccessfulEventHandler();
        public event ConnectionSuccessfulEventHandler OnConnectionSuccessful;

        public delegate void MessageReceivedEventHandler();
        public event MessageReceivedEventHandler OnMessageReceived;
    }
}
