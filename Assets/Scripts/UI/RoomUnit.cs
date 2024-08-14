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
        [SerializeField] private Text textCount;
        [SerializeField] private Button btnJoin;

        private RoomData data = null;

        public void Init(RoomData _data)
        {
            if (data == _data)
                return;

            data = _data;
            gameObject.SetActive(Convert.ToBoolean(_data.CanJoin));
            
            textName.text = _data.RoomName;
            textCount.text = $"{JsonConvert.DeserializeObject<UserData[]>(_data.Players).Length} 명";

            btnJoin.onClick.RemoveAllListeners();
            btnJoin.onClick.AddListener(Join);
        }

        public void Join()
        {
            List<UserData> members = JsonConvert.DeserializeObject<UserData[]>(data.Players).ToList();
            members.Add(LocalDataBase.instance.loginData);

            RoomData sendData = new RoomData()
            {
                RoomID = data.RoomID,
                RoomName = data.RoomName,
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
