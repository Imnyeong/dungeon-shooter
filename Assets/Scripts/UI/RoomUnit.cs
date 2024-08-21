using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System;

namespace DungeonShooter
{
    public class RoomUnit : MonoBehaviour
    {
        [SerializeField] private Text textName;
        [SerializeField] private Text textCanJoin;
        [SerializeField] private Text textCount;
        [SerializeField] private Button btnJoin;

        private RoomData data = null;

        public void Init(RoomData _data)
        {
            if (data == _data)
                return;

            data = _data;
            
            textName.text = _data.RoomName;
            textCanJoin.text = _data.CanJoin == 1 ? StringData.RoomCanJoin : StringData.RoomCantJoin;
            textCount.text = $"{JsonConvert.DeserializeObject<UserData[]>(_data.Players).Length} 명";

            btnJoin.onClick.RemoveAllListeners();
            btnJoin.interactable = Convert.ToBoolean(_data.CanJoin);
            btnJoin.onClick.AddListener(delegate
            {
                WebRequestManager.instance.GetRoomInfo(data.RoomID, (response) =>
                {
                    if (Convert.ToBoolean(data.CanJoin))
                        Join();
                    else
                        return;
                });
            });
        }

        public void Join()
        {
            List<UserData> members = JsonConvert.DeserializeObject<UserData[]>(data.Players).ToList();
            members.Add(LocalDataBase.instance.loginData);

            RoomData sendData = new RoomData()
            {
                RoomID = data.RoomID,
                RoomName = data.RoomName,
                CanJoin = data.CanJoin,
                MasterID = data.MasterID,
                Players = JsonConvert.SerializeObject(members.ToArray())
            };
            WebRequestManager.instance.ModifyRoom(sendData, (response) =>
            {
                LocalDataBase.instance.currentRoom = data.RoomID;
                LobbyCanvas.instance.ChangeView(ViewModelType.Room);               
            });
        }
    }
}
