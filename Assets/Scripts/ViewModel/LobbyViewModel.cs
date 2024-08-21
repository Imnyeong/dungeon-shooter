using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;

namespace DungeonShooter
{
    public class LobbyViewModel : ViewModel
    {
        public override ViewModelType type => ViewModelType.Lobby;
        [SerializeField] private ScrollRect roomList;
        [SerializeField] private GameObject roomUnit;

        [SerializeField] private Button btnRefresh;
        [SerializeField] private Button btnCreate;

        [SerializeField] private Text textEmpty;

        private void Start()
        {
            btnRefresh.onClick.AddListener(delegate
            {
                AudioManager.instance.PlayClip(StringData.ClipClick);
                GetRoomList();
            });
            btnCreate.onClick.AddListener(delegate
            {
                AudioManager.instance.PlayClip(StringData.ClipClick);
                LobbyCanvas.instance.ShowPopup(PopupType.CreateRoom);
            });
        }
        private void OnEnable()
        {
            GetRoomList();
        }
        public void GetRoomList()
        {
            WebRequestManager.instance.GetRoomList((response) => ShowRoomList(response));
        }
        public void ShowRoomList(WebRequestResponse _response)
        {
            if (_response.code == 400)
                return;

            RoomData[] dataList = JsonConvert.DeserializeObject<RoomData[]>(_response.message);

            for (int i = 0; i < roomList.content.childCount; i++)
            {
                Destroy(roomList.content.GetChild(i).gameObject);
            }

            textEmpty.gameObject.SetActive(dataList == null || dataList.Length == 0);

            for (int i = 0; i < dataList.Length; i++)
            {
                GameObject unit = Instantiate(roomUnit, roomList.content);
                unit.GetComponent<RoomUnit>().Init(dataList[i]);
            }
        }
    }
}