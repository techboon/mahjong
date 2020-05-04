using Mahjong.Domain;
using System.Collections.Generic;

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

        public void OnReadyRoom();
        public void OnStartGame();
        public void OnNext();
        public void OnYourDeck(Table table, Deck deck);
    }
}