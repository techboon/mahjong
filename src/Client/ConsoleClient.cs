using System;
using System.Collections.Generic;
using Grpc.Core;
using MagicOnion.Client;
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
        Table table;

        bool isGaming;

        public ConsoleClient(string hostname, int port)
        {
            // standard gRPC channel
            this.channel = new Channel(hostname, port, ChannelCredentials.Insecure);
            this.isGaming = false;
        }

        public void Run()
        {
            bool isContinue = true;

            while(isContinue)
            {
                string key = Console.ReadLine();
                if (!this.isGaming)
                {
                    isContinue = this.Command(key);
                } else {
                    isContinue = this.CommandInGame(key);
                }

                Console.WriteLine(channel.State.ToString());
            }
        }

        public bool Command(string key)
        {
            switch(key)
            {
                case "Q":
                    return false;
                case "C":
                    this.hub = StreamingHubClient.Connect<IGameHub, IGameHubReceiver>(this.channel, this);
                    break;
                case "J":
                    Console.WriteLine("Enter User ID:");
                    string uid = Guid.NewGuid().ToString();
                    string name = Console.ReadLine();
                    this.hub.JoinAsync(uid, name);
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
                case "S":
                    this.hub.StartGameAsync();
                    break;
                default:
                    break;
            }
            return true;
        }

        public bool CommandInGame(string key)
        {
            if ("Q" == key)
            {
                return false;
            }
            if (this.player.Uid == this.table.NowPlaying.Uid)
            {
                Tile t = Tile.FromString(key);
                if (null != t)
                {
                    this.hub.InGameDahai(t);
                }
            }
            return true;
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

        public void OnStartGame()
        {
            Console.WriteLine("対局開始");
            this.isGaming = true;
            this.hub.GetDeckAsync();
        }

        public void OnNext()
        {
            this.hub.GetDeckAsync();
        }

        public void OnYourDeck(Table table, Deck deck)
        {
            this.table = table;
            Console.WriteLine("{0}さんの手番", table.NowPlaying.Name);
            Console.WriteLine("手牌", table.NowPlaying);
            foreach (Tile t in deck.tiles)
            {
                Console.Write("[{0}] ", t.ToString());
            }
            Console.WriteLine();
        }
    }
}
