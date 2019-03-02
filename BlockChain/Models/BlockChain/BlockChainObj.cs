using System;
using System.Collections.Generic;

namespace BlockChain.Models.BlockChain
{
    public class BlockChainObj
    {
        /// <summary>
        /// The Chain that makes up our Block Chain
        /// </summary>
        List<Block> chain;

        /// <summary>
        /// Block Chain constructor
        /// Both creates the chain and the genesis block
        /// </summary>
        public BlockChainObj()
        {
            chain = new List<Block>();
            chain.Add(CreateGenesisBlock());
        }

        /// <summary>
        /// Creates the Genesis block
        /// </summary>
        /// <returns></returns>
        public Block CreateGenesisBlock()
        {
            Block genesisBlock = new Block(DateTime.Now, "");
            genesisBlock.hash = genesisBlock.GenerateHash();

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
            Block latestBlock = GetLatestBlock();
            currentBlock.index = latestBlock.index + 1;
            currentBlock.previousHash = latestBlock.hash;
            currentBlock.hash = currentBlock.GenerateHash();
            chain.Add(currentBlock);
        }

    }
}
