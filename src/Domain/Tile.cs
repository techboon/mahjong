using MessagePack;
using System;

namespace Mahjong.Domain
{
    [MessagePackObject]
    public class Tile {
        public enum Type
        {
            Man,
            Pin,
            Soh,
            Fon,
            Sangen
        }
        [Key(0)]
        public Type type { get; private set; }
        [Key(1)]
        public int value { get; private set; }

        public Tile(Type type, int value)
        {
            this.type = type;
            this.value = value;
        }
    }
}
