using UnityEngine;
using WebSocketSharp;
using System.Collections.Concurrent;
using System;

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
                ws.Send("O");
            }
            if (Input.GetKeyDown(KeyCode.P))
            {
                ws.Send("P");
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
                //Debug.Log("주소 :  " + ((WebSocket)sender).Url + ", 데이터 : " + e.Data);
                
                if (e.Data == "O")
                {
                    actions.Enqueue(() => GameManager.instance.SpawnCharacter(0));
                }
                else if(e.Data == "P")
                {
                    actions.Enqueue(() => GameManager.instance.SpawnCharacter(1));
                }
                else
                {
                    actions.Enqueue(() => GameManager.instance.MoveCharacter(e.Data));
                }
            };
        }


    }
}