using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonShooter
{
    public class Character : MonoBehaviour
    {
        public Collider col;
        public Rigidbody rigid;

        public int id;
        public int hp;

        public CharacterInput inputController;
        public CharacterAnimation animController;

        private void LateUpdate()
        {
            if (id != GameManager.instance.currentPlayer)
                return;
            SendPacket();
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
            TransformPacket packet = new TransformPacket()
            {
                id = id,
                position = transform.localPosition,
                rotation = transform.localRotation
            };
            WebSocketRequest request = new WebSocketRequest()
            {
                packetType = PacketType.Transform,
                data = JsonUtility.ToJson(packet)
            };
            WebSocketManager.instance.SendPacket(JsonUtility.ToJson(request));
        }
        public void TransformSync(Vector3 _position, Quaternion _rotation)
        {
            transform.localPosition = _position;
            transform.localRotation = _rotation;
        }
    }
}