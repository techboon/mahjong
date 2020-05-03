using ConsoleAppFramework;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace mahjong
{
    class Program : ConsoleAppBase
    {
        static async Task Main(string[] args)
        {
            await Host.CreateDefaultBuilder().RunConsoleAppFrameworkAsync<Program>(args);
        }
        public void Run(bool runServer = false, string hostname = "localhost", int port = 9999)
        {
            if (runServer)
            {
                this.runServer(hostname, port);
            } else {
                this.runClient();
            }
        }

        private void runServer(string hostname, int port)
        {
            Console.WriteLine("server host:{0} port:{1}", hostname, port);
        }

        private void runClient()
        {
            Console.WriteLine("client");
        }
    }
}
