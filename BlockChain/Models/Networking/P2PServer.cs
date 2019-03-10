using System;
using System.Net;
using System.Net.Sockets;

namespace BlockChain.Models.Networking
{
    public class P2PServer
    {
        #region FieldsAndProps
        public TcpListener server;
        public TcpClient client;
        IPEndPoint ip;

        bool IsConnected { get; set; } = false;
        int Port = 0;
        bool serverListening = false;
        #endregion


        #region Constructor
        /// <summary>
        /// Setup the server, scanning for availale port
        /// </summary>
        /// <param name="port"></param>
        public P2PServer(int port)
        {
            Port = port;
        }
        #endregion


        #region Methods
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
            OnConnectionSuccessful?.Invoke(new ClientFromServerEventArgs(client));
            IsConnected = true;
        }
        #endregion


        #region DelegatesAndEvents
        public delegate void OpenPortEventHandler();
        public event OpenPortEventHandler OnPortOpen;

        public delegate void ConnectionSuccessfulEventHandler(ClientFromServerEventArgs e);
        public event ConnectionSuccessfulEventHandler OnConnectionSuccessful;

        public delegate void MessageReceivedEventHandler();
        #endregion
    }

    public class ClientFromServerEventArgs : EventArgs
    {

        public ClientFromServerEventArgs(TcpClient Client)
        {
            client = Client;
        }

        public TcpClient client;
    }
}
