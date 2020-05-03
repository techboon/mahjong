using MessagePack;
using System;
using System.Collections.Generic;

namespace Mahjong.Domain
{
    [MessagePackObject]
    public class Player
    {
        [Key(0)]
        public string Name { get; private set; }
        [Key(1)]
        public List<Tile> Deck { get; private set; }

        public Player(string name)
        {
            this.Name = name;
            this.Deck = new List<Tile>();
        }

        public void addDeck(Tile tile)
        {
            this.Deck.Add(tile);
            Console.WriteLine(tile.type.ToString() + tile.value.ToString());
        }
    }
}
