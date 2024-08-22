using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DungeonShooter
{
    public class RoomViewModel : ViewModel
    {
        public override ViewModelType type => ViewModelType.Room;

        [SerializeField] private Text textRoomName;

        [SerializeField] private ScrollRect memberList;
        [SerializeField] private GameObject memberUnit;

        [SerializeField] private Button btnStart;
        [SerializeField] private Button btnEixt;

        private RoomData data = null;

        private void Start()
        {
            btnStart.onClick.AddListener(delegate
            {
                AudioManager.instance.PlayClip(StringData.ClipClick);
                StartGame();
            });
            btnEixt.onClick.AddListener(delegate
            {
                AudioManager.instance.PlayClip(StringData.ClipClick);
                if (data.MasterID == LocalDataBase.instance.loginData.ID)
                    DeleteRoom();
                else
                    ExitRoom();
            });
        }
        private void OnEnable()
        {
            if(!WebSocketManager.instance.isConnect())
            {
                ConnectRoom();
            }
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
            btnStart.gameObject.SetActive(LocalDataBase.instance.loginData.ID == data.MasterID);

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
        public void StartGame()
        {
            RoomData sendData = new RoomData()
            {
                RoomID = data.RoomID,
                RoomName = data.RoomName,
                CanJoin = 0,
                MasterID = data.MasterID,
                Players = data.Players
            };
            WebRequestManager.instance.ModifyRoom(sendData, (response) =>
            {
                WebSocketRequest request = new WebSocketRequest()
                {
                    packetType = PacketType.Game,
                    data = StringData.GameStart
                };
                WebSocketManager.instance.SendPacket(JsonUtility.ToJson(request));
                LoadInGameScene();
            });
        }
        public void LoadInGameScene()
        {
            SceneManager.LoadScene(StringData.SceneInGame);
        }
        public void KickedRoom()
        {
            data = null;
            WebSocketManager.instance.Disconnect();
            LocalDataBase.instance.currentRoom = default;
            LobbyCanvas.instance.ChangeView(ViewModelType.Lobby);
        }
        public void ExitRoom()
        {
            List<UserData> players = JsonConvert.DeserializeObject<UserData[]>(data.Players).ToList();
           
            players.RemoveAll(x => x.ID == LocalDataBase.instance.loginData.ID);

            RoomData sendData = new RoomData()
            {
                RoomID = data.RoomID,
                RoomName = data.RoomName,
                CanJoin = data.CanJoin,
                MasterID = data.MasterID,
                Players = JsonConvert.SerializeObject(players.ToArray())
            };
            WebRequestManager.instance.ModifyRoom(sendData, (response) =>
            {
                DisconnectRoom(StringData.RoomExit);
                LocalDataBase.instance.currentRoom = default;
                LobbyCanvas.instance.ChangeView(ViewModelType.Lobby);
            });
        }
        public void DeleteRoom()
        {
            WebRequestManager.instance.DeleteRoom(data, (response) =>
            {
                DisconnectRoom(StringData.RoomDelete);
                LocalDataBase.instance.currentRoom = default;
                LobbyCanvas.instance.ChangeView(ViewModelType.Lobby);
            });
        }
        public void ConnectRoom()
        {
            WebSocketManager.instance.Connect();
            WebSocketRequest request = new WebSocketRequest()
            {
                packetType = PacketType.Room,
                data = StringData.RoomJoin
            };
            WebSocketManager.instance.SendPacket(JsonUtility.ToJson(request));
        }
        public void DisconnectRoom(string _data)
        {
            WebSocketRequest request = new WebSocketRequest()
            {
                packetType = PacketType.Room,
                data = _data
            };
            WebSocketManager.instance.SendPacket(JsonUtility.ToJson(request));
            WebSocketManager.instance.Disconnect();
        }
    }
}