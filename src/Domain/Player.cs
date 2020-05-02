using System;
using System.Collections.Generic;

namespace Mahjong.Domain
{
    class Player
    {
        public string Name { get; private set; }
        private List<Tile> Deck;

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
