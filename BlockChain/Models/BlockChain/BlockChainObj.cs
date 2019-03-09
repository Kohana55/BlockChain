﻿using BlockChain.Models.Networking;
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

        // To be replaced with "P2PClient" class
        public P2PServer server;



        /// <summary>
        /// 
        /// </summary>
        public Stopwatch hashTimer;

        /// <summary>
        /// Block Chain constructor
        /// Both creates the chain and the genesis block
        /// </summary>
        public BlockChainObj(P2PServer server)
        {
            this.server = server;
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
            Block genesisBlock = new Block(DateTime.Now, "");
            genesisBlock.MineHash(nugget);
            genesisBlock.previousHash = "GenesisBlock";
            genesisBlock.data = "GenesisBlock";
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
        /// Adds a block to the chain
        /// </summary>
        public void AddBlock(Block currentBlock)
        {
            StatusUpdate?.Invoke(this, "Mining Block...");

            // Init a timer for timing the hash generation process
            hashTimer = new Stopwatch();
            
            // Add block
            Block latestBlock = GetLatestBlock();
            currentBlock.index = latestBlock.index + 1;
            currentBlock.previousHash = latestBlock.hash;
            hashTimer.Start();
            currentBlock.MineHash(nugget);
            hashTimer.Stop();
            chain.Add(currentBlock);    
        }

        public void ProcessTransaction(Transaction transaction)
        {
            transaction.RunHash();
            transactionPool.AddTransaction(transaction);

            // send on network
            server.Send(transaction.Serialise());
        }

        public delegate void OnStatusUpdateEventHandler(object sender, string e);
        public event OnStatusUpdateEventHandler StatusUpdate;
    }
}
