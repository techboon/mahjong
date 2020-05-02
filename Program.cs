using System;
using System.Collections.Generic;
using Mahjong.Domain;

namespace mahjong
{
    class Program
    {
        static void Main(string[] args)
        {
            Player a = new Player("A");
            Player b = new Player("B");
            Player c = new Player("C");
            Player d = new Player("D");
            Table t = Table.Make(new List<Player>() {a, b, c, d});

            Console.WriteLine("配牌完了");
        }
    }
}
