using Grpc.Core;
using MagicOnion.Server;
using Mahjong.Client;
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
        public void Run(bool server = false, string hostname = "localhost", int port = 9999)
        {
            GrpcEnvironment.SetLogger(new Grpc.Core.Logging.ConsoleLogger());

            if (server)
            {
                this.runServer(hostname, port);
            } else {
                this.runClient(hostname, port);
            }
        }

        private void runServer(string hostname, int port)
        {
            Console.WriteLine("server host:{0} port:{1}", hostname, port);
            var service = MagicOnionEngine.BuildServerServiceDefinition(isReturnExceptionStackTraceInErrorDetail: true);
            var server = new global::Grpc.Core.Server
            {
                Services = { service },
                Ports = { new ServerPort(hostname, port, ServerCredentials.Insecure) }
            };
            
            // launch gRPC Server.
            server.Start();

            // and wait.
            Console.ReadLine();
        }

        private void runClient(string hostname, int port)
        {
            Console.WriteLine("client host:{0} port:{1}", hostname, port);
            ConsoleClient cli = new ConsoleClient(hostname, port);
            cli.Run();
        }
    }
}
