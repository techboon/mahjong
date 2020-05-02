using System;
using System.Linq;
using System.Collections.Generic;
using Mahjong.Helper;
using Mahjong.Exception;

namespace Mahjong.Domain
{
    class Table
    {
        public Player SheetTon { get; private set; }
        public Player SheetNan { get; private set; }
        public Player SheetSha { get; private set; }
        public Player SheetPey { get; private set; }

        public List<Tile> Dora;

        private List<Tile> Tiles;

        private Table(List<Player> players)
        {
            if (3 > players.Count || 4 < players.Count)
            {
                throw new InvalidArgumentException();
            }

            List<Player> shuffledPlayers = players.OrderBy(a => Guid.NewGuid()).ToList();
            this.SheetTon = shuffledPlayers[0];
            this.SheetNan = shuffledPlayers[1];
            this.SheetSha = shuffledPlayers[2];
            if (4 == players.Count)
            {
                this.SheetPey = shuffledPlayers[3];
            }

            this.Dora = new List<Tile>();
            this.Tiles = CreateAllTile.Invoke().OrderBy(a => Guid.NewGuid()).ToList();

            this.Haipai();
        }

        static public Table Make(List<Player> players)
        {
            return new Table(players);
        }

        private void Haipai()
        {
            // 開門位置の正しさは気にしない
            // (演出上そうすればいい)
            // 最後から2枚目を最後と入れ替え(降ろす)
            Tile tempLast = this.Tiles[this.Tiles.Count - 1 - 1];
            this.Tiles.RemoveAt(this.Tiles.Count - 1 - 1);
            this.Tiles.Add(tempLast);

            // 最後から6枚目がドラ
            this.Dora.Add(this.Tiles[this.Tiles.Count - 1 - 6]);

            Player[] p;
            if (null == this.SheetPey)
            {
                p = new Player[] { this.SheetTon, this.SheetNan, this.SheetSha };
            } else {
                p = new Player[] { this.SheetTon, this.SheetNan, this.SheetSha, this.SheetPey };
            }
            int pos = 0;

            // 東家から順に4枚ずつ3回取る
            for (int i = 0; i < 3; i++)
            {
                foreach (Player target in p)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        target.addDeck(this.Tiles[pos++]);
                    }
                }
            }
            // 1枚ずつ取る
            foreach (Player target in p)
            {
                target.addDeck(this.Tiles[pos++]);
            }
            // 東家が最後にちょんちょん取る2枚目の分
            this.SheetTon.addDeck(this.Tiles[pos++]);

            this.Tiles = this.Tiles.GetRange(pos, (this.Tiles.Count - pos));
        }
    }
}
