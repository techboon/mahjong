using System;

namespace Mahjong.Domain
{
    class Tile {
        public enum Type
        {
            Man,
            Pin,
            Soh,
            Fon,
            Sangen
        }

        public Type type { get; private set; }
        public int value { get; private set; }

        public Tile(Type type, int value)
        {
            this.type = type;
            this.value = value;
        }
    }
}
