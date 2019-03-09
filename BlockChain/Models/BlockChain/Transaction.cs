using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BlockChain.Models.BlockChain
{
    public class Transaction
    {

        public string sender;
        public string receiver;
        public string amount;
        public DateTime dateTime;
        public string transactionHash;


        /// <summary>
        /// Constructs a transaction
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="Receiver"></param>
        /// <param name="Amount"></param>
        public Transaction(string Sender, string Receiver, string Amount, DateTime dateTime)
        {
            sender = Sender;
            receiver = Receiver;
            amount = Amount;
            dateTime = DateTime.Now;
            RunHash();
        }


        /// <summary>
        /// Runs a hash on this transaction that other nodes 
        /// can check against their transaction pools
        /// </summary>
        public void RunHash()
        {
            SHA256 sha256 = SHA256.Create();

            byte[] inputBytes = Encoding.ASCII.GetBytes($"{dateTime}-{sender}-{receiver}-{amount}");
            byte[] outputBytes = sha256.ComputeHash(inputBytes);

            transactionHash = Convert.ToBase64String(outputBytes);
        }


        /// <summary>
        /// Returns this transaction as a string for sending on the network
        /// </summary>
        /// <returns></returns>
        public string Serialise()
        {
            return $"TRANSACTION:{sender},{receiver},{amount},{dateTime},{transactionHash}" + 0;
        }
    }
}
