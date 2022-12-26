using Microsoft.AspNetCore.Hosting.Server;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using WebSocketSharp;

namespace Coin.Models.Concrete
{
    public class P2PClient
    {
        public IDictionary<string, WebSocket> wsDict = new Dictionary<string, WebSocket>();
        public void Connect(string url)
        {
            if (!wsDict.ContainsKey(url))
            {
                WebSocket ws = new WebSocket(url);
                ws.OnMessage += (sender, e) =>
                {
                    if (e.Data == "Merhaba Client")
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
                };
                ws.Connect();
                ws.Send(JsonConvert.SerializeObject(Program.MainBlockChain));
                wsDict.Add(url, ws);
            }
        }

        public void Send(string url, string data)
        {
            foreach (var item in wsDict)
            {
                if (item.Key == url)
                {
                    item.Value.Send(data);
                }
            }
        }

        public void Broadcast(string data)
        {
            foreach (var item in wsDict)
            {
                item.Value.Send(data);
            }
        }

        public IList<string> GetServers()
        {
            IList<string> servers = new List<string>();
            foreach (var item in wsDict)
            {
                servers.Add(item.Key);
            }
            return servers;
        }

        public void Close()
        {
            foreach (var item in wsDict)
            {
                item.Value.Close();
            }
        }

    }
}
