using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace BlockChain.Models.Networking
{
    public class P2PServer
    {

        TcpListener server;
        TcpClient client;

        bool isConnected = false;

        public P2PServer(int port)
        {
            server = new TcpListener(IPAddress.Parse("127.0.0.1"), port);            
            ListenForConnection();
        }

        /// <summary>
        /// Blocks Thread using a blocking call "AcceptTcpClient()"
        /// </summary>
        public void ListenForConnection()
        {
            try
            {
                server.Start();
                client = server.AcceptTcpClient();
            }
            catch (Exception ex)
            {

            }

            isConnected = true;
        }
    }
}
