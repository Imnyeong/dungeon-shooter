using UnityEngine;
using UnityEngine.UI;

namespace DungeonShooter
{
    public class MemberUnit : MonoBehaviour
    {
        [SerializeField] private Text textName;
        [SerializeField] private GameObject textMaster;

        private UserData data;

        public void Init(UserData _data, bool _isMaster)
        {
            if (data == _data)
                return;

            data = _data;

            textName.text = _data.Nickname;
            textMaster.SetActive(_isMaster);
        }
    }
}
