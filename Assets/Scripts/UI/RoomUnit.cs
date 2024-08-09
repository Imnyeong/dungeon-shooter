using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;

namespace DungeonShooter
{
    public class RoomUnit : MonoBehaviour
    {
        [SerializeField] private Text textName;
        [SerializeField] private Text textCount;

        public void Init(RoomData _data)
        {
            textName.text = _data.RoomName;
            string[] players = JsonConvert.DeserializeObject<string[]>(_data.Players);
            textCount.text = $"{players.Length} 명";
        }
    }
}
