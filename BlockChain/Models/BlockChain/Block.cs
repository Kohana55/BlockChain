using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace BlockChain.Models.BlockChain
{
    /// <summary>
    /// A Block 
    /// </summary>
    public class Block
    {

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
         * The fields needed for a block are;
         * 
         * An Index number
         * A Timestamp
         * The Hash Code of the previous block
         * Its own Hash Code
         * And of course, the "data" - in this case, a string with some value
         * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public int index;
        public DateTime date;
        public string previousHash;
        public string hash;
        public int nonce;
        public string data;
        public List<Transaction> transactions;

        /// <summary>
        /// Create a block using the date and the data
        /// The remaining information is set to null as it'll be replaced
        /// during the "AddBlock" process
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="dataArg"></param>
        public Block(DateTime dt, string dataArg, List<Transaction> transactions)
        {
            index = 0;
            date = dt;
            previousHash = null;
            hash = null;
            nonce = 0;
            data = dataArg;
            this.transactions = transactions;
        }


        /// <summary>
        /// Builds a network block
        /// </summary>
        /// <param name="networkBlockBuffer"></param>
        public Block(string networkBlockBuffer)
        {//$"B:{index},{date},{previousHash},{hash},{nonce},{data}";

            string[] tokens = networkBlockBuffer.Split(',');

            // Index
            index = int.Parse(tokens[0]);

            // Parse DateTime
            string[] dateTimeTokens = tokens[1].Split(' ');
            string[] dateTokens = dateTimeTokens[0].Split('/');
            string[] timeTokens = dateTimeTokens[1].Split(':');
            date = new DateTime(int.Parse(dateTokens[2]), int.Parse(dateTokens[1]), int.Parse(dateTokens[0]),
                        int.Parse(timeTokens[0]), int.Parse(timeTokens[1]), int.Parse(timeTokens[2]));

            // Hash Codes
            previousHash = tokens[2];
            hash = tokens[3];
            nonce = int.Parse(tokens[4]);

            transactions = new List<Transaction>();
            string[] transactionDataArray = tokens[5].Split('|');
            for (int i = 0; i<transactionDataArray.Length; i++)
            {
                transactions.Add(new Transaction(transactionDataArray[i]));
            }
        }

        /// <summary>
        /// Generate a hash code using SHA256
        /// </summary>
        /// <returns>A string holding the hash value</returns>
        public string GenerateHash()
        {
            SHA256 sha256 = SHA256.Create();

            byte[] inputBytes = Encoding.ASCII.GetBytes($"{date}-{previousHash ?? ""}-{data}-{nonce}");
            byte[] outputBytes = sha256.ComputeHash(inputBytes);

            return Convert.ToBase64String(outputBytes);
        }

        /// <summary>
        /// Mines for a hash code that contains the input
        /// from user. 
        /// 
        /// If no input is entered, the nugget defaults to "Lew"
        /// Thus - "LewCoins"
        /// </summary>
        public void MineHash(string nugget)
        {
            // If nugget field is left blank, use "Lew"
            if (nugget == null)
                nugget = "Lew";

            BuildDataString();

            // Mine for Hash
            bool hashMined = false;
            while (hashMined == false)
            {
                nonce++;
                hash = GenerateHash();

                for (int i = 0; (i < hash.Length - nugget.Length); i++)
                {
                    if (hash.Substring(i, nugget.Length) == nugget)
                        hashMined = true;
                }
            }
        }

        private void BuildDataString()
        {
            // Transactions can be null when building the Genesis Block
            if (transactions == null)
                return;

            // Build Data string to be hashed
            for (int i = 0; i < transactions.Count; i++)
            {
                data = string.Concat(data, transactions[0].Serialise());

                if (!(i == transactions.Count - 1)) { data = string.Concat(data, "|"); }
            }
        }

        public string Serialise()
        {
            return $"B:{index},{date},{previousHash},{hash},{nonce},{data}";
        }
    }
}
