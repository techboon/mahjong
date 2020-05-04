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

        public bool Equal(Tile tile)
        {
            return this.type == tile.type && this.value == tile.value;
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

        static public Tile FromString(string s)
        {
            switch (s)
            {
                case "東":
                    return new Tile(Type.Fon, 1);
                case "南":
                    return new Tile(Type.Fon, 2);
                case "西":
                    return new Tile(Type.Fon, 3);
                case "北":
                    return new Tile(Type.Fon, 4);
                case "白":
                    return new Tile(Type.Sangen, 1);
                case "撥":
                    return new Tile(Type.Sangen, 2);
                case "中":
                    return new Tile(Type.Sangen, 3);
                default:
                    break;
            }
            if (2 != s.Length)
            {
                return null;
            }
            int value;
            if (!int.TryParse(s[0].ToString(), out value))
            {
                return null;
            }
            if (0 > value || 9 < value)
            {
                return null;
            }
            Type? type = ParseTypeFromChar(s[1]);
            if (!type.HasValue)
            {
                return null;
            }
            return new Tile(type.Value, value);
        }

        static private Type? ParseTypeFromChar(char c)
        {
            switch (c)
            {
                case 'm':
                    return Type.Man;
                case 'p':
                    return Type.Pin;
                case 's':
                    return Type.Soh;
                default:
                    return null;
            }
        }
    }
}
