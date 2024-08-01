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
    public class TransformPacket
    {
        public int id;

        public float posX;
        public float posY;
        public float posZ;
        public float rotX;
        public float rotY;
        public float rotZ;
    }

    #endregion
}
