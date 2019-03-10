using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockChain.Models.BlockChain;
using BlockChain.Models.Networking;

namespace BlockChain.Models
{
    public class Main
    {
        /// <summary>
        /// Lew Coins!!!! 
        /// </summary>
        public BlockChainObj lewCoins;
        public P2PServer server;
        public P2PClient client;

        /// <summary>
        /// Entry point for our programme
        /// 
        /// (Business logic, code behind, models...
        /// ...whatever you kids are calling it these days)
        /// </summary>
        public Main()
        {
            server = new P2PServer(1000);
            client = new P2PClient();
            server.OnConnectionSuccessful += SetClientFromServer;
            lewCoins = new BlockChainObj(client);         
            Task serverConnection = Task.Run(() => server.Start());
        }

        /// <summary>
        /// Set client from server
        /// </summary>
        /// <param name="e"></param>
        private void SetClientFromServer(ClientFromServerEventArgs e)
        {
            client.client = e.client;
            Task clientConnection = Task.Run(() => client.StartReceiving());

            // Send current Hash on network
            client.Send($"U:{lewCoins.GetLatestBlock().hash},{lewCoins.GetLatestBlock().index}");
        }
    }
}
