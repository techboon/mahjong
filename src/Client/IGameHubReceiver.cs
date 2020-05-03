using Mahjong.Server;

namespace Mahjong.Client
{
    /// <summary>
    /// Server -> ClientのAPI
    /// </summary>
    public interface IGameHubReceiver
    {
        /// <summary>
        /// 誰かがチャットに参加したことをクライアントに伝える。
        /// </summary>
        /// <param name="name">参加した人の名前</param>
        public void OnJoin(string name);

        /// <summary>
        /// 誰かがチャットから退室したことをクライアントに伝える。
        /// </summary>
        /// <param name="name">退室した人の名前</param>
        public void OnLeave(string name);

        /// <summary>
        /// 誰かが発言した事をクライアントに伝える。
        /// </summary>
        /// <param name="name">発言した人の名前</param>
        /// <param name="message">メッセージ</param>
        public void OnReceiveMessage(string name, string message);

        public void OnEnterRoom(Room room);
    }
}