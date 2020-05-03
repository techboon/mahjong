using MagicOnion;
using Mahjong.Client;
using System.Threading.Tasks;

namespace Mahjong.Server
{
    /// <summary>
    /// CLient -> ServerのAPI
    /// </summary>
    public interface IGameHub : IStreamingHub<IGameHub, IGameHubReceiver>
    {
        /// <summary>
        /// 参加することをサーバに伝える
        /// </summary>
        /// <param name="userName">参加者の名前</param>
        /// <returns></returns>
        Task JoinAsync(int uid);
        /// <summary>
        /// 退室することをサーバに伝える
        /// </summary>
        /// <returns></returns>
        Task LeaveAsync();
        /// <summary>
        /// メッセージをサーバに伝える
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        Task SendMessageAsync(string message);
    }
}