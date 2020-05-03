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
        Task JoinAsync(string uid);
        Task LeaveAsync();
        Task SendMessageAsync(string message);
        Task CreateRoomAsync();
        Task EnterRoomAsync(string roomId);
        Task SendMessageInRoomAsync(string message);

        Task SitDownAsync();
        Task StandUpAsync();
        Task RefreshRoomAsync();

        Task StartGameAsync();
        Task GetDeckAsync();
    }
}