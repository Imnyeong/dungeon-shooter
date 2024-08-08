using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonShooter
{
    public class Character : MonoBehaviour
    {
        public Collider hitCol;
        public Collider bottomCol;
        public Rigidbody rigid;

        public int id;
        public int hp;

        private CharacterInput inputController;
        private CharacterAnimation animController;

        private void LateUpdate()
        {
            if (id != GameManager.instance.currentPlayer)
                return;
            SendPacket();
        }
        public void OnTriggerEnter(Collider other)
        {
            if (inputController != null)
            { 
                inputController.InGround();
            }
        }
        public void SetInfo(int _id)
        {
            id = _id;
            if (GameManager.instance.currentPlayer == _id)
            {
                inputController = gameObject.AddComponent<CharacterInput>();
            }
            animController = gameObject.AddComponent<CharacterAnimation>();
        }
        private void SendPacket()
        {
            CharacterPacket packet = new CharacterPacket()
            {
                id = id,
                position = transform.localPosition,
                rotation = transform.localRotation,
                animation = animController.animationType
            };
            WebSocketRequest request = new WebSocketRequest()
            {
                packetType = PacketType.Character,
                data = JsonUtility.ToJson(packet)
            };
            WebSocketManager.instance.SendPacket(JsonUtility.ToJson(request));
        }
        public void DoSync(Vector3 _position, Quaternion _rotation, AnimationType _anim)
        {
            transform.localPosition = _position;
            transform.localRotation = _rotation;
            animController.DoAnimation(_anim);
        }
    }
}