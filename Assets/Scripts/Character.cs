using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonShooter
{
    public class Character : MonoBehaviour
    {
        public Collider col;
        public Rigidbody rigid;
        public Animator animator;

        public int id;
        public int hp;
        public float moveSpeed = 10.0f;

        public float moveX;
        public float moveZ;

        private void Update()
        {
            if (id != GameManager.instance.currentPlayer)
                return;
            GetAxisValues();
        }
        private void FixedUpdate()
        {
            if (id != GameManager.instance.currentPlayer)
                return;
            Move();
        }
        private void LateUpdate()
        {
            if (id != GameManager.instance.currentPlayer)
                return;
            SendPacket();
        }
        public void SetInfo(int _id)
        {
            id = _id;
        }
        private void GetAxisValues()
        {
            moveX = Input.GetAxisRaw("Horizontal");
            moveZ = Input.GetAxisRaw("Vertical");
        }
        private void Move()
        {
            transform.Translate(moveZ * Vector3.forward * Time.fixedDeltaTime * moveSpeed);
            transform.Translate(moveX * Vector3.right * Time.fixedDeltaTime * moveSpeed);
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