using System;
using System.Collections.Generic;
using Mahjong.Domain;

namespace Mahjong.Helper
{
    static class CreateAllTile
    {
        static public List<Tile> Invoke()
        {
            List<Tile> tiles = new List<Tile>();
            Tile.Type[] normals = { Tile.Type.Man, Tile.Type.Pin, Tile.Type.Soh};

            for (int c = 0; c < 4; c++)
            {
                foreach (Tile.Type t in  normals)
                {
                    for (int n = 1; n < 9; n++)
                    {
                        tiles.Add(new Tile(t, n));
                    }
                }

                for (int n = 1; n < 4; n++)
                {
                    tiles.Add(new Tile(Tile.Type.Fon, n));
                }

                for (int n = 1; n < 3; n++)
                {
                    tiles.Add(new Tile(Tile.Type.Sangen, n));
                }
            }
            return tiles;
        }
    }
}
