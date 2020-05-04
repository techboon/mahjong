using MessagePack;
using System;
using System.Collections.Generic;

namespace Mahjong.Domain
{
    [MessagePackObject]
    public class Player
    {
        [Key(0)]
        public string Uid { get; private set; }
        [Key(1)]
        public string Name { get; private set; }
        [Key(2)]
        public Deck Deck { get; private set; }

        public Player(string uid, string name)
        {
            this.Uid = uid;
            this.Name = name;
            this.Deck = new Deck();
        }

        public void addDeck(Tile tile)
        {
            this.Deck.Add(tile);
            Console.WriteLine(tile.type.ToString() + tile.value.ToString());
        }
    }
}
