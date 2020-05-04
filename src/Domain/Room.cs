using MessagePack;
using System.Collections.Generic;
using System.Threading;

namespace Mahjong.Domain
{
    [MessagePackObject]
    public class Room
    {
        [Key(0)]
        public string id { get; set; }
        [Key(1)]
        public List<Player> Players { get; set; }
        [Key(2)]
        public List<Player> SheetPlayers { get; set; }

        private Table Table;

        public Room(string id)
        {
            this.id = id;
            this.Players = new List<Player>();
            this.SheetPlayers = new List<Player>();
        }

        public void Join(Player player)
        {
            this.Players.Add(player);
        }

        public void Leave(Player player)
        {
            this.Players.Remove(player);
        }

        public bool SitDown(Player player)
        {
            if (this.SheetPlayers.Contains(player))
            {
                return true;
            }
            lock(this.SheetPlayers)
            {
                if (4 > this.SheetPlayers.Count)
                {
                    this.SheetPlayers.Add(player);
                    return true;
                }
            }
            return false;
        }

        public void StandUp(Player player)
        {
            if (this.SheetPlayers.Contains(player))
            {
                this.SheetPlayers.Remove(player);
            }
        }

        public bool IsSitting(Player player)
        {
            foreach (Player p in this.SheetPlayers)
            {
                if (p.Uid == player.Uid)
                {
                    return true;
                }
            }
            return false;
        }

        public void Start()
        {
            Interlocked.CompareExchange(ref this.Table, new Table(this.SheetPlayers), null);
        }

        public bool IsStarted()
        {
            return null != this.Table;
        }

        public Table GetTable()
        {
            return this.Table;
        }
    }
}
