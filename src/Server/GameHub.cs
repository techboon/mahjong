using Mahjong.Client;
using MagicOnion.Server.Hubs;
using System.Threading.Tasks;

namespace Mahjong.Server
{
    /// <summary>
    /// CLient -> ServerのAPI
    /// </summary>
    public class GameHub : StreamingHubBase<IGameHub, IGameHubReceiver>, IGameHub
    {
        string uid;
        IGroup globalGroup;
        IGroup roomGroup;
        Room room;

        public async Task JoinAsync(string uid)
        {
            this.uid = uid;
            this.globalGroup = await Group.AddAsync("global");
            await Task.Run(() => Broadcast(this.globalGroup).OnJoin(uid));
        }
        public async Task LeaveAsync()
        {
            await this.globalGroup.RemoveAsync(this.Context);
            await Task.Run(() => Broadcast(this.globalGroup).OnLeave(uid.ToString()));
        }
        public async Task SendMessageAsync(string message)
        {
            await Task.Run(() => Broadcast(this.globalGroup).OnReceiveMessage(uid, message));
        }

        public async Task CreateRoomAsync()
        {
            RoomProvider roomProvider = RoomProvider.Singleton();
            if (null == this.roomGroup)
            {
                this.room = await Task.Run(() => roomProvider.CreateRoom());
                this.roomGroup = await Group.AddAsync(this.room.id);
            }
            await Task.Run(() => BroadcastToSelf(this.globalGroup).OnEnterRoom(this.room));
        }

        public async Task EnterRoomAsync(string roomId)
        {
            RoomProvider roomProvider = RoomProvider.Singleton();
            this.room = roomProvider.Get(roomId);
            this.roomGroup = await Group.AddAsync(this.room.id);
            await Task.Run(() => BroadcastToSelf(this.globalGroup).OnEnterRoom(this.room));
        }

        public async Task SendMessageInRoomAsync(string message)
        {
            await Task.Run(() => Broadcast(this.roomGroup).OnReceiveMessage(uid, message));
        }
    }
}