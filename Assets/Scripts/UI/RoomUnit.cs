using UnityEngine;
using UnityEngine.UI;

namespace DungeonShooter
{
    public class RoomUnit : MonoBehaviour
    {
        [SerializeField] private Text textName;
        [SerializeField] private Text textCount;

        public void Init(RoomData _data)
        {
            textName.text = _data.RoomName;
            //if(_data.Players == null)
            //ToDo : 인원 카운트
            textCount.text = _data.Players;
        }
    }
}
