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
        public Camera characterCamera;

        public int id { get; private set; }
        public int hp { get; private set; }
        public float moveSpeed = 10.0f;
        public float mouseSpeed = 15.0f;

        public float moveX;
        public float moveZ;

        public float mouseX;
        //public float mouseY;
        //public float maxX = 35.0f;
        //public float minX = -35.0f;

        private void Update()
        {
            if (id != GameManager.instance.currentPlayer)
                return;
            //현재 id와 같은 플레이어만 조작
            GetAxisValues();
        }
        private void FixedUpdate()
        {
            MouseRotate();
            Move();
        }
        private void LateUpdate()
        {
            if (moveX != 0 || moveZ != 0 || mouseX != 0)
                SendPacket();
        }
        public void SetInfo(int _id)
        {
            id = _id;
            transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
        }
        private void GetAxisValues()
        {
            moveX = Input.GetAxisRaw("Horizontal");
            moveZ = Input.GetAxisRaw("Vertical");

            mouseX += Input.GetAxisRaw("Mouse X");
            //mouseY += Input.GetAxisRaw("Mouse Y");
        }

        private void Move()
        {
            //Vector3 moveDir = new Vector3(moveX, 0.0f, moveZ);
            //Vector3 moveVector = moveDir.normalized * Time.fixedDeltaTime * moveSpeed;
            //rigid.MovePosition(rigid.position + moveVector);
            transform.Translate(moveZ * Vector3.forward * Time.fixedDeltaTime * moveSpeed);
            transform.Translate(moveX * Vector3.right * Time.fixedDeltaTime * moveSpeed);
        }
        private void MouseRotate()
        {
            //mouseY = Mathf.Clamp(mouseY, -45f, 45f);
            //characterCamera.transform.localRotation = Quaternion.Euler(mouseY, mouseX, 0);
            transform.localRotation = Quaternion.Euler(0, mouseX, 0);
            //characterCamera.transform.localRotation = Quaternion.Euler(mouseY, 0, 0);
            //transform.Rotate(0, mouseX * mouseSpeed, 0);
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