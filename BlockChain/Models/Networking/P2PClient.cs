using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;


namespace BlockChain.Models.Networking
{
    class P2PClient
    {

        TcpClient client;

        public P2PClient(int port)
        {
            client = new TcpClient();
            client.Connect(IPAddress.Parse("127.0.0.1"), port);
        }

        public void Connect(String server, String message)
        {
 
        }

        public void Send()
        {

        }

        public void Receive()
        {

        }
    }
}
