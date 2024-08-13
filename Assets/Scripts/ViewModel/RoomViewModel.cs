using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace DungeonShooter
{
    public class RoomViewModel : ViewModel
    {
        public override ViewModelType type => ViewModelType.Room;

        [SerializeField] private Text textRoomName;

        [SerializeField] private ScrollRect memberList;
        [SerializeField] private GameObject memberUnit;

        [SerializeField] private Button btnEixt;
        [SerializeField] private Button btnStart;

        private RoomData data;

        private void Start()
        {
            btnEixt.onClick.AddListener(delegate
            {
                if (data.MasterID == LocalDataBase.instance.loginData.ID)
                    DeleteRoom();
                else
                    ExitRoom();
            });
            btnStart.onClick.AddListener(delegate
            {
                
            });
        }
        private void OnEnable()
        {
            GetRoomInfo(LocalDataBase.instance.currentRoom);
        }
        public void GetRoomInfo(int _id)
        {
            WebRequestManager.instance.GetRoomInfo(_id, (response) => SetRoomInfo(response));
        }
        public void SetRoomInfo(WebRequestResponse _response)
        {
            if (_response.code == 400)
                return;

            data = JsonConvert.DeserializeObject<RoomData>(_response.message);

            textRoomName.text = data.RoomName;
            UserData[] players = JsonConvert.DeserializeObject<UserData[]>(data.Players);

            for (int i = 0; i < memberList.content.childCount; i++)
            {
                Destroy(memberList.content.GetChild(i).gameObject);
            }
            for (int i = 0; i < players.Length; i++)
            {
                GameObject unit = Instantiate(memberUnit, memberList.content);
                unit.GetComponent<MemberUnit>().Init(players[i], players[i].ID == data.MasterID);
            }
        }
        public void ExitRoom()
        {
            List<UserData> players = JsonConvert.DeserializeObject<UserData[]>(data.Players).ToList();
           
            players.RemoveAll(x => x.ID == LocalDataBase.instance.loginData.ID);

            RoomData sendData = new RoomData()
            {
                RoomID = data.RoomID,
                RoomName = data.RoomName,
                MasterID = data.MasterID,
                Players = JsonConvert.SerializeObject(players.ToArray())
            };
            WebRequestManager.instance.ModifyRoom(sendData, (response) =>
            {
                LocalDataBase.instance.currentRoom = default;
                LobbyCanvas.instance.ChangeView(ViewModelType.Lobby);
            });
        }
        public void DeleteRoom()
        {
            WebRequestManager.instance.DeleteRoom(data, (response) =>
            {
                LocalDataBase.instance.currentRoom = default;
                LobbyCanvas.instance.ChangeView(ViewModelType.Lobby);
            });
        }
    }
}