using System;
using System.Collections.Concurrent;
using Mahjong.Exception;

namespace Mahjong.Server
{
    class RoomProvider
    {
        static RoomProvider self;

        private ConcurrentDictionary<string, Room> Rooms;

        private RoomProvider()
        {
            this.Rooms = new ConcurrentDictionary<string, Room>();
        }

        static public RoomProvider Singleton()
        {
            if (null == self) {
                self = new RoomProvider();
            }
            return self;
        }

        public Room CreateRoom()
        {
            Room room = null;
            for (int i = 0; i < 3; i++)
            {
                string roomId = Guid.NewGuid().ToString();
                if (!this.Rooms.TryAdd(roomId, new Room(roomId)))
                {
                    continue;
                }
                room = this.Rooms[roomId];
            }
            if (null == room)
            {
                throw new TryLimitException();
            }
            return room;
        }

        public Room Get(string roomId)
        {
            return this.Rooms[roomId];
        }
    }
}