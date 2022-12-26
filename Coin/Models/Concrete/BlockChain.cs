using EntityLayer.Concrete;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Coin.Models.Concrete
{
    public class BlockChain
    {
        public IList<Transaction> PendingTransactions = new List<Transaction>();
        public IList<Block> Chain { get; set; }
        public int difficulty { get; set; } = 2;
        public int Reward { get; set; } = 5;

        public BlockChain() 
        {
            InitializeChain();
            AddGenesisBlock();
        }

        public void InitializeChain()
        {
            Chain = new List<Block>();
        }

        public Block CreateGenesisBlock()
        {
            Block block = new Block(DateTime.Now, null, PendingTransactions);
            block.Mine(difficulty);
            PendingTransactions = new List<Transaction>();

            return block;
        }

        // Chai'nin baslangic blogu
        public void AddGenesisBlock()
        {
            Chain.Add(CreateGenesisBlock());
        }

        public Block GetLatestBlock()
        {
            return Chain[Chain.Count - 1];
        }

        public void AddBlock(Block block)
        {
            var latestBlock = GetLatestBlock();
            block.Index = latestBlock.Index +1;
            block.PrevHash = latestBlock.Hash;
            block.Hash = block.CalculateHash();
            block.Mine(this.difficulty);
            Chain.Add(block);
        }

        public void CreateTransaction(Transaction transaction)
        {
            PendingTransactions.Add(transaction);
        }

        // Kullanıcıların Database'de bulunan Rc lerini BlockChain'imiz ile esliyoruz
        public void UpdateBlockChainWithDatabase(List<Users> userList)
        {
            if (this.Chain.Count < 2)
            {
                for (int i = 0; i < userList.Count; i++)
                {
                    CreateTransaction(new Transaction("Admin - User's Database RC Syncron", userList[i].UsersAdress, userList[i].RecycleCoin));
                }
                Block block = new Block(DateTime.Now, GetLatestBlock().Hash, PendingTransactions);
                AddBlock(block);
                PendingTransactions = new List<Transaction>();
            }
        }

        // CarbonCoinleri RC'ye Donustururken Kullanıyoruz
        public void ConvertCarbonCoinToRecycleCoin(string minerAdress, int coinToAdd)
        {
            CreateTransaction(new Transaction("Admin - Carbon to Recycle Coin", minerAdress, coinToAdd));
            Block block = new Block(DateTime.Now, GetLatestBlock().Hash, PendingTransactions);
            AddBlock(block);
            PendingTransactions = new List<Transaction>();
        }

        // Bekleyen transactionlari blockchaine isliyoruz
        public void ProcessPendingTransactions(string minerAdress)
        {
            CreateTransaction(new Transaction("Admin - Mining Reward", minerAdress, Reward));
            Block block = new Block(DateTime.Now, GetLatestBlock().Hash, PendingTransactions);
            AddBlock(block);
            PendingTransactions= new List<Transaction>();
        }

        // Chainimizin dogrulugunu kontrol ediyoruz.
        public bool IsValid()
        {
            for (int i = 1; i < Chain.Count; i++)
            {
                Block currentBlock = Chain[i];
                Block previousBlock = Chain[i - 1];
                if (currentBlock.Hash != currentBlock.CalculateHash())
                {
                    return false;
                }
                if (currentBlock.PrevHash != previousBlock.Hash)
                {
                    return false;
                }
            }
            return true;
        }

        // Kullanicilarin chain uzerindeki balance'ini getiriyor
        public int GetBalance(string adress)
        {
            int balance = 0;

            for (int i = 0; i < Chain.Count; i++)
            {
                for (int j = 0; j < Chain[i].Transactions.Count; j++)
                {
                    var transaction = Chain[i].Transactions[j];
                    if (transaction.FromAdress == adress)
                    {
                        balance -= transaction.Amount;
                    }
                    if (transaction.ToAdress == adress)
                    {
                        balance += transaction.Amount;
                    }
                }
            }
            return balance;
        }
    }
}
