using System;
using System.Collections.Generic;
using Grpc.Core;
using MagicOnion.Client;
using Mahjong.Client;
using Mahjong.Domain;
using Mahjong.Server;

namespace Mahjong.Client
{
    class ConsoleClient : IGameHubReceiver
    {
        Channel channel;
        IGameHub hub;
        Player player;
        Room room;

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
                        this.hub = StreamingHubClient.Connect<IGameHub, IGameHubReceiver>(this.channel, this);
                        break;
                    case "J":
                        Console.WriteLine("Enter User ID:");
                        string uid = Console.ReadLine();
                        this.hub.JoinAsync(uid);
                        break;
                    case "E":
                        this.hub.CreateRoomAsync();
                        break;
                    case "F":
                        Console.WriteLine("Enter Room ID:");
                        string roomId = Console.ReadLine();
                        this.hub.EnterRoomAsync(roomId);
                        break;
                    case "M":
                        Console.WriteLine("Enter Message (global):");
                        string message = Console.ReadLine();
                        this.hub.SendMessageAsync(message);
                        break;
                    case "RM":
                        Console.WriteLine("Enter Message (room):");
                        string roomMessage = Console.ReadLine();
                        this.hub.SendMessageInRoomAsync(roomMessage);
                        break;
                    case "RS":
                        if (null == this.room)
                        {
                            Console.WriteLine("ルームに入室していない");
                            continue;
                        }
                        if (this.room.IsSitting(this.player))
                        {
                            this.hub.StandUpAsync();
                        } else {
                            this.hub.SitDownAsync();
                        }
                        break;
                    case "RR":
                        this.hub.RefreshRoomAsync();
                        break;
                    default:
                        break;
                }
                Console.WriteLine(channel.State.ToString());
            }
        }
        public void OnJoin(string name)
        {
            GrpcEnvironment.Logger.Debug("Joined user: {0}", name);
        }

        public void OnJoinComplete(Player player)
        {
            this.player = player;
        }

        public void OnLeave(string name)
        {

        }

        public void OnReceiveMessage(string name, string message)
        {
            Console.WriteLine("{0}: {1}", name, message);
        }

        public void OnEnterRoom(Room room)
        {
            this.room = room;
            GrpcEnvironment.Logger.Debug("Entered room id: {0}", room.id);
        }

        public void OnRoomUpdate(Room room)
        {
            this.room = room;
            GrpcEnvironment.Logger.Debug("room updated");
        }

        public void OnReadyRoom()
        {
            Console.WriteLine("対局開始できる人数になりました");
        }
    }
}