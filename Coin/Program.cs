using BusinessLayer.Abstract;
using Coin.Models.Concrete;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coin
{
    public class Program
    {
        public static BlockChain MainBlockChain = new BlockChain();
        public static int Port = 0;
        public static P2PClient Client = new P2PClient();
        public static P2PServer Server = null;
        public static void Main(string[] args)
        {
            if (args.Length >= 1)
            {
                Port = int.Parse(args[0]);
            }

            if (Port > 0)
            {
                Server = new P2PServer();
                Server.Start();
            }
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
