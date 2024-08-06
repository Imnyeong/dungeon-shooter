using UnityEngine;

namespace DungeonShooter
{
    #region WebRequestPacket
    public class WebRequestResponse
    {
        public int code;
        public string message;
    }
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
    #endregion

    #region WebSocketPacket
    public enum PacketType
    {
        Spawn,
        Transform
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
    public class TransformPacket
    {
        public int id;
        public Vector3 position;
        public Quaternion rotation;
    }
    #endregion
}
