using System;
using UnityEngine;

namespace DungeonShooter
{
    #region Enum
    public enum ViewModelType
    {
        Login,
        Register,
        Lobby
    }
    public enum PopupType
    {
        Error,
        CreateRoom
    }
    #endregion
    #region WebRequestPacket
    public class WebRequestResponse
    {
        public int code;
        public string message;
    }
    [Serializable]
    public class UserData
    {
        public string ID;
        public string PW;
        public string Nickname;
        public int Win = 0;
        public int Lose = 0;
        public int Gold = 0;
    }
    public class LoginData
    {
        public string ID;
        public string PW;
    }

    [Serializable]
    public class RoomData
    {
        public string RoomName;
        public string MasterID;
        public string Players;
    }
    #endregion

    #region WebSocketPacket
    public enum PacketType
    {
        Spawn,
        Character
    }
    public enum AnimationType
    {
        Idle,
        Move,
        Back,
        Jump
    }
    public class WebSocketRequest
    {
        public PacketType packetType;
        public string data;
    }
    public class WebSocketResponse
    {
        public PacketType packetType;
        public string data;
    }
    public class CharacterPacket
    {
        public int id;
        public Vector3 position;
        public Quaternion rotation;
        public AnimationType animation;
    }
    #endregion
}
