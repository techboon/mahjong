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
        public enum FonValue
        {
            Ton = 1,
            Nan,
            Sha,
            Pey
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

        public override string ToString()
        {
            switch (this.type)
            {
                case Type.Man:
                    return this.value + "m";
                case Type.Pin:
                    return this.value + "p";
                case Type.Soh:
                    return this.value + "s";
                case Type.Fon:
                    string[] fpai = {"東", "南", "西", "北"};
                    return fpai[this.value - 1];
                case Type.Sangen:
                    string[] spai = {"白", "撥", "中"};
                    return spai[this.value - 1];
                default:
                    return "?";
            }
        }
    }
}
