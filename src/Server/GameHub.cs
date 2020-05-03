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
        int uid;
        IGroup globalGroup;

        public async Task JoinAsync(int uid)
        {
            this.uid = uid;
            this.globalGroup = await Group.AddAsync(uid.ToString());
        }
        public async Task LeaveAsync()
        {
            await this.globalGroup.RemoveAsync(this.Context);
            await Task.Run(() => Broadcast(this.globalGroup).OnLeave(uid.ToString()));
        }
        public async Task SendMessageAsync(string message)
        {
            await Task.Run(() => Broadcast(this.globalGroup).OnReceiveMessage(uid.ToString(), message));
        }
    }
}