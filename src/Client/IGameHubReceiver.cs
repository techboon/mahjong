using Mahjong.Domain;

namespace Mahjong.Client
{
    /// <summary>
    /// Server -> ClientのAPI
    /// </summary>
    public interface IGameHubReceiver
    {
        public void OnJoinComplete(Player player);
        public void OnJoin(string name);
        public void OnLeave(string name);
        public void OnReceiveMessage(string name, string message);

        public void OnEnterRoom(Room room);
        public void OnRoomUpdate(Room room);
    }
}