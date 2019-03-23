using BlockChain.Models.Networking;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace BlockChain.Models.BlockChain
{
    public class BlockChainObj
    {
        /// <summary>
        /// The Chain that makes up our Block Chain
        /// </summary>
        public List<Block> chain;
        public string nugget;
        public TransactionPool transactionPool;
        public P2PClient client;



        /// <summary>
        /// 
        /// </summary>
        public Stopwatch hashTimer;

        /// <summary>
        /// Block Chain constructor
        /// Both creates the chain and the genesis block
        /// </summary>
        public BlockChainObj(P2PClient client)
        {
            this.client = client;
            client.OnMessageReceived += MessageReceivedFromClient;
            chain = new List<Block>();
            transactionPool = new TransactionPool();
            chain.Add(CreateGenesisBlock());
        }

        /// <summary>
        /// Creates the Genesis block
        /// </summary>
        /// <returns></returns>
        public Block CreateGenesisBlock()
        {
            Block genesisBlock = new Block(DateTime.Now, "", null);
            genesisBlock.MineHash(nugget);
            genesisBlock.previousHash = "GenesisBlock";
            genesisBlock.data = "GenesisBlock";
            genesisBlock.transactions = new List<Transaction>();
            return genesisBlock;
        }

        /// <summary>
        /// Returns the latest block
        /// </summary>
        /// <returns>The latest block in the chain</returns>
        public Block GetLatestBlock()
        {
            return chain[chain.Count - 1];
        }

        /// <summary>
        /// Creates a new block from network message
        /// then validates it - if all passes
        /// Add to block chain
        /// </summary>
        public void ProcessBlockFromNetwork(string networkBlock)
        {
            Block potentialBlock = new Block(networkBlock);

            // Validate here...

            if(GetLatestBlock().hash == potentialBlock.previousHash)
            {
                chain.Add(potentialBlock);
                ChainUpdated?.Invoke();
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="transaction"></param>
        public void ProcessTransaction(Transaction transaction)
        {
            transaction.RunHash();
            transactionPool.AddTransaction(transaction);

            // send on network
            if (client != null)
                client.Send(transaction.Serialise());
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        private void MessageReceivedFromClient(string message)
        {
            if (message.Substring(0, "B:".Length) == "B:")
            {
                string trimmedData = message.Substring(message.IndexOf(':') + 1);
                ProcessBlockFromNetwork(trimmedData);
                // Remove transactions from pool here
                client.Send("M:BlockAccepted");
                //client.Send($"U:{GetLatestBlock().hash},{GetLatestBlock().index}");
            }
        }


        public delegate void OnStatusUpdateEventHandler(object sender, string e);
        public event OnStatusUpdateEventHandler StatusUpdate;
        public delegate void OnChainUpdateEventHandler();
        public event OnChainUpdateEventHandler ChainUpdated;
    }
}
