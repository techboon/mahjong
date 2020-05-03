using Mahjong.Client;
using Mahjong.Domain;
using Mahjong.Helper;
using MagicOnion.Server.Hubs;
using System.Threading.Tasks;

namespace Mahjong.Server
{
    /// <summary>
    /// CLient -> ServerのAPI
    /// </summary>
    public class GameHub : StreamingHubBase<IGameHub, IGameHubReceiver>, IGameHub
    {
        Player player;
        IGroup globalGroup;
        IGroup roomGroup;
        Room room;

        public async Task JoinAsync(string uid)
        {
            this.player = new Player(uid);
            this.globalGroup = await Group.AddAsync("global");
            await Task.Run(() => BroadcastToSelf(this.globalGroup).OnJoinComplete(this.player));
            await Task.Run(() => BroadcastExceptSelf(this.globalGroup).OnJoin(uid));
        }
        public async Task LeaveAsync()
        {
            await this.globalGroup.RemoveAsync(this.Context);
            await Task.Run(() => Broadcast(this.globalGroup).OnLeave(this.player.Name.ToString()));
        }
        public async Task SendMessageAsync(string message)
        {
            await Task.Run(() => Broadcast(this.globalGroup).OnReceiveMessage(this.player.Name, message));
        }

        public async Task CreateRoomAsync()
        {
            RoomProvider roomProvider = RoomProvider.Singleton();
            if (null == this.roomGroup)
            {
                this.room = await Task.Run(() => roomProvider.CreateRoom());
                this.room.Join(this.player);
                this.roomGroup = await Group.AddAsync(this.room.id);
            }
            await Task.Run(() => BroadcastToSelf(this.globalGroup).OnEnterRoom(this.room));
        }

        public async Task EnterRoomAsync(string roomId)
        {
            RoomProvider roomProvider = RoomProvider.Singleton();
            this.room = roomProvider.Get(roomId);
            this.room.Join(this.player);
            this.roomGroup = await Group.AddAsync(this.room.id);
            await Task.Run(() => BroadcastToSelf(this.globalGroup).OnEnterRoom(this.room));
            await Task.Run(() => BroadcastExceptSelf(this.roomGroup).OnRoomUpdate(this.room));
        }

        public async Task SendMessageInRoomAsync(string message)
        { 
            await Task.Run(() => Broadcast(this.roomGroup).OnReceiveMessage(this.player.Name, message));
        }

        public async Task SitDownAsync()
        {
            bool result = this.room.SitDown(player);
            await Task.Run(() => Broadcast(this.roomGroup).OnRoomUpdate(this.room));
            if (result)
            {
                await Task.Run(() => Broadcast(this.roomGroup).OnReceiveMessage("SYS", this.player.Name + " さんが着席"));
                if (!this.room.IsStarted() && 3 <= this.room.SheetPlayers.Count)
                {
                    await Task.Run(() => Broadcast(this.roomGroup).OnReadyRoom());
                }
            } else {
                await Task.Run(() => BroadcastToSelf(this.roomGroup).OnReceiveMessage("SYS", "着席できませんでした"));
            }
        }

        public async Task StandUpAsync()
        {
            this.room.StandUp(player);
            await Task.Run(() => Broadcast(this.roomGroup).OnRoomUpdate(this.room));
            await Task.Run(() => Broadcast(this.roomGroup).OnReceiveMessage("SYS", this.player.Name + " さんが離席"));
        }

        public async Task RefreshRoomAsync()
        {
            await Task.Run(() => BroadcastToSelf(this.roomGroup).OnRoomUpdate(this.room));
        }

        public async Task StartGameAsync()
        {
            if (!this.room.IsStarted())
            {
                this.room.Start();
                await Task.Run(() => Broadcast(this.roomGroup).OnStartGame());
            }
        }

        public async Task GetDeckAsync()
        {
            await Task.Run(() => BroadcastToSelf(this.roomGroup).OnYourDeck(this.room.GetTable(), this.player.Deck));
        }
    }
}