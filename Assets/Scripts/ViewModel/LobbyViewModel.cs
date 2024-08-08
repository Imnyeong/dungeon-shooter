using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DungeonShooter
{
    public class LobbyViewModel : ViewModel
    {
        public override ViewModelType type => ViewModelType.Lobby;
        [SerializeField] private ScrollRect roomList;
        [SerializeField] private GameObject roomUnit;

        private void OnEnable()
        {
            GetRoomList();
        }
        public void GetRoomList()
        {
            WebRequestManager.instance.GetUserList((response) => ShowRoomList(response));
        }
        public void ShowRoomList(WebRequestResponse _response)
        {
            if (_response.code == 400)
                return;
            
            List<RoomData> userList = JsonConvert.DeserializeObject<List<RoomData>>(_response.message);
            for (int i = 0; i < roomList.content.childCount; i++)
            {
                Destroy(roomList.content.GetChild(i).gameObject);
            }
            for (int i = 0; i < userList.Count; i++)
            {
                GameObject unit = Instantiate(roomUnit, roomList.content);
                unit.GetComponent<RoomUnit>().Init(userList[i]);
            }
        }
    }
}