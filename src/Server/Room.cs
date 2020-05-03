using MessagePack;

namespace Mahjong.Server
{
    [MessagePackObject]
    public class Room
    {
        [Key(0)]
        public string id { get; private set; }

        public Room(string id)
        {
            this.id = id;
        }
    }
}