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
        WebSocket ws = new WebSocket("");
        private readonly ConcurrentQueue<Action> actions = new ConcurrentQueue<Action>();

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }
        private void Start()
        {
            Connect();
        }
        private void Update()
        {
            CheckAction();
            if(Input.GetKeyDown(KeyCode.O))
            {
                WebSocketRequest request = new WebSocketRequest()
                {
                    packetType = PacketType.Spawn,
                    data = "O"
                };
                ws.Send(JsonConvert.SerializeObject(request));
            }
            if (Input.GetKeyDown(KeyCode.P))
            {
                WebSocketRequest request = new WebSocketRequest()
                {
                    packetType = PacketType.Spawn,
                    data = "P"
                };
                ws.Send(JsonConvert.SerializeObject(request));
            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                if (GameManager.instance.currentPlayer == 0)
                    GameManager.instance.currentPlayer = 1;
                else if (GameManager.instance.currentPlayer == 1)
                    GameManager.instance.currentPlayer = 0;
            }
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
            ws.Connect();

            ws.OnMessage += (sender, e) =>
            {
                //Debug.Log("???? :  " + ((WebSocket)sender).Url + ", ?????? : " + e.Data);
                WebSocketResponse response = JsonConvert.DeserializeObject<WebSocketResponse>(e.Data);

                if (response.packetType == PacketType.Character)
                {
                    actions.Enqueue(() => GameManager.instance.SyncCharacters(response.data));
                }
                else if(response.packetType == PacketType.Spawn && response.data == "O")
                {
                    actions.Enqueue(() => GameManager.instance.SpawnCharacter(0));
                }
                else if(response.packetType == PacketType.Spawn && response.data == "P")
                {
                    actions.Enqueue(() => GameManager.instance.SpawnCharacter(1));
                }
            };
        }
        public void OnApplicationQuit()
        {
            ws.Close();
        }
    }
}