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
            inputRoomName.onValueChanged.AddListener(delegate { AudioManager.instance.PlayClip(StringData.ClipTyping); });

            btnCreate.onClick.AddListener(delegate
            {
                AudioManager.instance.PlayClip(StringData.ClipClick);
                CreateRoom();
            });
            btnCancel.onClick.AddListener(delegate
            {
                ClearData();
                AudioManager.instance.PlayClip(StringData.ClipClick);
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

                if (response.message == ResponseMessage.RoomListisFull)
                {
                    Debug.Log("RoomList is Full");
                    return;
                }
                LocalDataBase.instance.currentRoom = Convert.ToInt32(response.message);
                LobbyCanvas.instance.ChangeView(ViewModelType.Room);
                // ToDo: room¿¡ Á¢¼Ó
            });
        }
    }
}

