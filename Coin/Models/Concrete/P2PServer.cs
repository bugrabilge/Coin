using Newtonsoft.Json;
using System.Collections.Generic;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace Coin.Models.Concrete
{
    public class P2PServer : WebSocketBehavior
    {
        bool chainSynched = false;
        WebSocketServer wss = null;
        public void Start()
        {
            wss = new WebSocketServer($"ws://127.0.0.1:{Program.Port}");
            wss.AddWebSocketService<P2PServer>("/Blockchain");
            wss.Start();
        }
        protected override void OnMessage(MessageEventArgs e)
        {
            if (e.Data == "Merhaba Server")
            {

            }
            else
            {
                BlockChain newChain = JsonConvert.DeserializeObject<BlockChain>(e.Data);
                if (newChain.IsValid() && newChain.Chain.Count > Program.MainBlockChain.Chain.Count)
                {
                    List<Transaction> newTransactions = new List<Transaction>();
                    newTransactions.AddRange(newChain.PendingTransactions);
                    newTransactions.AddRange(Program.MainBlockChain.PendingTransactions);
                    newChain.PendingTransactions = newTransactions;
                    Program.MainBlockChain = newChain;
                }
            }

            if (!chainSynched)
            {
                Send(JsonConvert.SerializeObject(Program.MainBlockChain));
                chainSynched = true;
            }
        }
    }
}
