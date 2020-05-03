using System;
using Grpc.Core;
using MagicOnion.Client;
using Mahjong.Client;
using Mahjong.Server;

namespace Mahjong.Client
{
    class ConsoleClient : IGameHubReceiver
    {
        Channel channel;
        public ConsoleClient(string hostname, int port)
        {
            // standard gRPC channel
            this.channel = new Channel(hostname, port, ChannelCredentials.Insecure);
        }

        public void Run()
        {
            bool isContinue = true;

            while(isContinue)
            {
                string key = Console.ReadLine();
                switch(key)
                {
                    case "Q":
                        isContinue = false;
                        break;
                    case "C":
                        StreamingHubClient.Connect<IGameHub, IGameHubReceiver>(this.channel, this);
                        break;
                    default:
                        break;
                }
                Console.WriteLine(channel.State.ToString());
            }
        }
        public void OnJoin(string name)
        {

        }

        public void OnLeave(string name)
        {

        }

        public void OnReceiveMessage(string name, string message)
        {

        }
    }
}