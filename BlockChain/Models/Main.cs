using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockChain.Models.BlockChain;

namespace BlockChain.Models
{
    public class Main
    {
        /// <summary>
        /// Lew Coins!!!! 
        /// </summary>
        BlockChainObj lewCoins;

        /// <summary>
        /// Entry point for our programme
        /// 
        /// (Business logic, code behind, models...
        /// ...whatever you kids are calling it these days)
        /// </summary>
        public Main()
        {

            lewCoins = new BlockChainObj();

            lewCoins.AddBlock(new Block(DateTime.Now, "s:Lewis,r:Aaron,n:100"));
            lewCoins.AddBlock(new Block(DateTime.Now, "s:Lewis,r:Aaron,n:600"));
            lewCoins.AddBlock(new Block(DateTime.Now, "s:Lewis,r:Aaron,n:300"));
            lewCoins.AddBlock(new Block(DateTime.Now, "s:Lewis,r:Aaron,n:2200"));
            lewCoins.AddBlock(new Block(DateTime.Now, "s:Lewis,r:Aaron,n:670"));
            lewCoins.AddBlock(new Block(DateTime.Now, "s:Lewis,r:Aaron,n:17"));
            lewCoins.AddBlock(new Block(DateTime.Now, "s:Lewis,r:Aaron,n:940"));
        }

    }
}
