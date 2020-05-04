using MessagePack;
using System.Collections.Generic;

namespace Mahjong.Domain
{
    [MessagePackObject]
    public class Deck
    {
        [Key(0)]
        public List<Tile> tiles { get; set; }

        public Deck()
        {
            this.tiles = new List<Tile>();
        }

        public void Add(Tile tile)
        {
            this.tiles.Add(tile);
        }

        public void Remove(Tile.Type type, int value)
        {
            Tile target = this.tiles.Find((x) => x.type == type && x.value == value);
            this.tiles.Remove(target);
        }

        public void Remove(Tile tile)
        {
            foreach (Tile t in this.tiles)
            {
                if (t.Equal(tile))
                {
                    this.tiles.Remove(t);
                    break;
                }
            }
        }

        public void Sort()
        {
            this.tiles.Sort((x, y) => x.value - y.value);
            this.tiles.Sort((x, y) => x.type - y.type);
        }

        public bool Has(Tile tile)
        {
            foreach (Tile t in this.tiles)
            {
                if (t.Equal(tile))
                {
                    return true;
                }
            }
            return false;
        }
    }
}