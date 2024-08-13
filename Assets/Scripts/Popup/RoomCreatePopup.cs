using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DungeonShooter
{
    public class RoomCreatePopup : Popup
    {
        public override PopupType type => PopupType.CreateRoom;

        [SerializeField] private InputField inputRoomName;

        [SerializeField] private Button btnCreate;
        [SerializeField] private Button btnCancel;

        private void Start()
        {
            btnCreate.onClick.AddListener(delegate
            {
                CreateRoom();
            });
            btnCancel.onClick.AddListener(delegate
            {
                ClearData();
                LobbyCanvas.instance.HidePopup(PopupType.CreateRoom);
            });
        }

        private void ClearData()
        {
            inputRoomName.text = string.Empty;
        }

        private void CreateRoom()
        { 
            List<UserData> players = new List<UserData>();
            players.Add(LocalDataBase.instance.loginData);

            RoomData sendData = new RoomData()
            {
                RoomName = inputRoomName.text,
                MasterID = LocalDataBase.instance.loginData.ID,
                Players = JsonConvert.SerializeObject(players.ToArray())
            };
            WebRequestManager.instance.CreateRoom(sendData, (response) => 
            {
                ClearData();
                LobbyCanvas.instance.HidePopup(PopupType.CreateRoom);
                LocalDataBase.instance.currentRoom = Convert.ToInt32(response.message);
                LobbyCanvas.instance.ChangeView(ViewModelType.Room);
                // ToDo: room¿¡ Á¢¼Ó
            });
        }
    }
}

