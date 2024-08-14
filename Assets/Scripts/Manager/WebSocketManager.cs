using UnityEngine;
using WebSocketSharp;
using System.Collections.Concurrent;
using System;
using Newtonsoft.Json;

namespace DungeonShooter
{
    public class WebSocketManager : MonoBehaviour
    {
        public static WebSocketManager instance;
        private const string wsURL = "";
        WebSocket ws; 
        private readonly ConcurrentQueue<Action> actions = new ConcurrentQueue<Action>();

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }
        private void Update()
        {
            CheckAction();
            //WebSocketRequest request = new WebSocketRequest()
            //{
            //    packetType = PacketType.Spawn,
            //    data = "O"
            //};
            //ws.Send(JsonConvert.SerializeObject(request));
        }
        public void OnApplicationQuit()
        {
            ws.Close();
        }
        public void SendPacket(string _message)
        {
            ws.Send(_message);
        }
        private void CheckAction()
        {
            while (actions.Count > 0)
            {
                if (actions.TryDequeue(out var _action))
                {
                    _action?.Invoke();
                }
            }
        }
        public void Connect()
        {
            ws = new WebSocket($"");
            ws.Connect();

            ws.OnMessage += (sender, e) =>
            {
                //Debug.Log("???? :  " + ((WebSocket)sender).Url + ", ?????? : " + e.Data);
                WebSocketResponse response = JsonConvert.DeserializeObject<WebSocketResponse>(e.Data);

                switch (response.packetType)
                {
                    case PacketType.Character:
                    {
                        actions.Enqueue(() => GameManager.instance.SyncCharacters(response.data));
                        break;
                    }
                    case PacketType.Room:
                    {
                        actions.Enqueue(delegate 
                        {
                            RoomViewModel room = LobbyCanvas.instance.FindViewModel(ViewModelType.Room).GetComponent<RoomViewModel>();
                            room.GetRoomInfo(LocalDataBase.instance.currentRoom);
                        });
                        break;
                    }
                }
                //actions.Enqueue(() => GameManager.instance.SpawnCharacter(0));
            };
        }
        public void Disconnect()
        {
            ws.Close();
        }
    }
}