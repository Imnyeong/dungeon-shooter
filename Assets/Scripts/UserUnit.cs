using UnityEngine;
using UnityEngine.UI;

namespace DungeonShooter
{
    public class UserUnit : MonoBehaviour
    {
        [SerializeField] private Text textID;
        [SerializeField] private Text textNickname;

        public void Init(UserData _data)
        {
            textID.text = _data.ID;
            textNickname.text = _data.Nickname;
        }
    }
}
