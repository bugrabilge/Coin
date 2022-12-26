using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Coin.Models.Concrete
{
    public class Block
    {
        public int Index { get; set; }
        public IList<Transaction> Transactions { get; set; }
        public string Hash { get; set; }
        public int Nonce { get; set; }
        public string PrevHash { get; set; }
        public DateTime TimeStamp { get; set; }

        public Block(DateTime timeStamp, string prevHash, IList<Transaction> transactions)
        {
            Index = 0;
            Transactions = transactions;
            Nonce = 0;
            PrevHash = prevHash;
            TimeStamp = timeStamp;
        }

        public string CalculateHash()
        {
            SHA256 sha256 = SHA256.Create();
            byte[] inBytes = Encoding.ASCII.GetBytes($"{TimeStamp}-{PrevHash ?? ""}-{JsonConvert.SerializeObject(Transactions)}-{Nonce}");
            byte[] outBytes = sha256.ComputeHash( inBytes );
            return Convert.ToBase64String( outBytes );

        }

        public void Mine(int difficulty)
        {
            var leadingZero = new string('0', difficulty);
            while (this.Hash == null || this.Hash.Substring(0, difficulty) != leadingZero)
            {
                this.Nonce++;
                this.Hash = this.CalculateHash();
            }
        }
    }
}
